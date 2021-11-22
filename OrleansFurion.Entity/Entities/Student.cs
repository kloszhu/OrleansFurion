using Furion.DatabaseAccessor;
using System.ComponentModel.DataAnnotations;

namespace OrleansFurion.Core.Entities
{
    public class Student: IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
       
    }
}
