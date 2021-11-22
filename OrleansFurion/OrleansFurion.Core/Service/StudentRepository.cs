using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using OrleansFurion.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrleansFurion.Core.Service
{
    public class StudentRepository : ITransient, IStudentRepository
    {
        private IRepository<Student> _repository;

        public StudentRepository(IRepository<Student> repository)
        {
            this._repository = repository;
        }

        public async Task SaveAsync(Student student)
        {
            await _repository.InsertNowAsync(student);
        }
        public IEnumerable<Student> List()
        {
            return _repository.AsEnumerable();
        }
    }
}
