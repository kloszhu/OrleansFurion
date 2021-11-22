using Orleans;
using OrleansFurion.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrleansFurion.Core.Orleans
{

    public interface IStudentManager: IGrainWithIntegerKey
    {
        Task<IEnumerable<Student>> ListAsync();
        Task SaveAsync(int key, Student student);
        //Task<int> MaxKeyAsync();
    }
}