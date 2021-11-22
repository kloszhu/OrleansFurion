using Furion.DynamicApiController;
using Orleans;
using OrleansFurion.Core.Entities;
using OrleansFurion.Core.Orleans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrleansFurion.Application
{
    /// <summary>
    /// 系统服务接口
    /// </summary>
    public class SystemAppService : IDynamicApiController
    {
        private readonly ISystemService _systemService;
        //private readonly IStudentRepository studentRepository;
        private IClusterClient _clusterClient;
        private IStudentManager manager;

        public SystemAppService(IClusterClient clusterClient,ISystemService systemService)
        {
            _systemService = systemService;
            //this.studentRepository = studentRepository;
            _clusterClient = clusterClient;
            manager = _clusterClient.GetGrain<IStudentManager>(1);
        }

        public async Task SaveAsync(Student student) {
            //await  studentRepository.SaveAsync(student);
            // var max = await manager.MaxKeyAsync();
            Random random = new Random(); 
           await manager.SaveAsync(random.Next(), student);
        }

        public async Task<IEnumerable<Student>> ListAsync() {
            return await manager.ListAsync();
           //return studentRepository.List();
        }
        /// <summary>
        /// 获取系统描述
        /// </summary>
        /// <returns></returns>
        public string GetDescription()
        {
            return _systemService.GetDescription();
        }
    }
}
