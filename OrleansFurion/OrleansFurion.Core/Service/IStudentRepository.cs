using OrleansFurion.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace OrleansFurion.Core.Service
{
    public interface IStudentRepository
    {
        IEnumerable<Student> List();
        Task SaveAsync(Student student);
    }
}