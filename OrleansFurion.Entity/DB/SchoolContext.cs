using Microsoft.EntityFrameworkCore;
using OrleansFurion.Core.Entities;
namespace OrleansFurion.Entity.DB
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=./Furion.db");
        }

   
        //实体
        public DbSet<Student> Students { get; set; }
    }
}
