using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Orleans;
using Orleans.Runtime;
using OrleansFurion.Core.Entities;
using OrleansFurion.Entity.DB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrleansFurion.Core.Orleans
{
    //[StorageProvider(ProviderName = "business")]
    public class StudentManager : Grain,ITransient,IStudentManager
    {
        ///1.mongo
        //private IPersistentState<List<Student>> _state;
        //private SchoolContext _dbContext;
        private IRepository<Student> repository;
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="repository"></param>
        public StudentManager(IRepository<Student> repository)
        {
            this.repository = repository;
        }

        //public StudentManager([PersistentState("votes", "votes")] IPersistentState<List<Student>> state,

        //    )
        //{
        //    _state = state;

        //}

        //public override Task OnActivateAsync()
        //{
        //    //_dbContext = new SchoolContext();
        //    return base.OnActivateAsync();
        //}


        public async Task<IEnumerable<Student>> ListAsync()
        {
            //await _state.ReadStateAsync();
            //return await Task.FromResult(_state.State.ToList());
            //return _dbContext.Students.ToList();
          return await Task.FromResult(repository.Entities.ToList());
        }

        //public async Task<int> MaxKeyAsync()
        //{
        //    return await Task.FromResult(this.State.Keys.Max());
        //}

        public async Task SaveAsync(int key, Student student)
        {
            await repository.InsertNowAsync(student);
            //_state.State.Add(student);
            //await _state.WriteStateAsync();
        }

    }
}
