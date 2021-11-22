using Furion;
using Microsoft.Extensions.DependencyInjection;

namespace OrleansFurion.EntityFramework.Core
{
    public class Startup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
                options.AddDbPool<DefaultDbContext>();
            }, "OrleansFurion.Database.Migrations");
        }
    }
}