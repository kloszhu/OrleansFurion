using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using OrleansFurion.Core.Orleans;
using OrleansFurion.Entity.DB;
using OrleansFurion.EntityFramework.Core;

namespace OrleansFurion.Web.Entry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)

                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.Inject()
                                  .UseStartup<Startup>()
                    ;
                    }).UseOrleans(ConfigureSiloBuilder);

        private static void ConfigureSiloBuilder(Microsoft.Extensions.Hosting.HostBuilderContext ctx, ISiloBuilder builder)
        {

            if (ctx.HostingEnvironment.IsDevelopment())
            {
                //builder.UseLocalhostClustering();
                //builder.AddMemoryGrainStorage("votes");
                //var redisAddress = $"{Environment.GetEnvironmentVariable("REDIS")}:6379";
                //  builder.UseLocalhostClustering();

                // var redisAddress = "redis://localhost";
                ///3.默认集群管理器
                builder.UseMongoDBClustering(options =>
                {
                    options.DatabaseName = "dbcluster"; options.CreateShardKeyForCosmos = false;

                });
                builder.ConfigureServices(services => {
                //services.AddEntityFrameworkSqlite().AddDbContext<SchoolContext>();
                //services.AddDbContext<SchoolContext>();
                //services.AddSingleton(typeof(SchoolContext));

                //services.AddInject();
                services.AddDatabaseAccessor(options =>
                {
                    options.AddDbPool<DefaultDbContext>();
                }, "OrleansFurion.Database.Migrations");
                    services.AddTransient<DefaultDbContext>();
                });
                ///2.Configration
                builder.Configure<ClusterOptions>(options => {
                    options.ClusterId = "ClusterId";
                    options.ServiceId = "ServiceId";
                });
                ///1.DB Connect
                builder.UseMongoDBClient("mongodb://localhost:27017");
                /// builder.UseMongoDBMembershipTable("mongodb:localhost:27017");
                /// 4.存储
                builder.AddMongoDBGrainStorage("votes", options =>
                {
                    options.DatabaseName = "dbcluster"; options.CreateShardKeyForCosmos = false;

                });

                builder.AddMongoDBGrainStorage("business", options =>
                {
                    options.DatabaseName = "business"; options.CreateShardKeyForCosmos = false;

                });
                builder.AddMongoDBGrainStorage("provider", options =>
                {
                    options.DatabaseName = "provider"; options.CreateShardKeyForCosmos = false;

                });



            }
            else
            {
                // In Kubernetes, we use environment variables and the pod manifest
                //builder.UseKubernetesHosting();

                //// Use Redis for clustering & persistence
                //var redisAddress = $"{Environment.GetEnvironmentVariable("REDIS")}:6379";
                //builder.UseRedisClustering(options => options.ConnectionString = redisAddress);
                //builder.AddRedisGrainStorage("votes", options => options.ConnectionString = redisAddress);
            }
            builder.ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(StudentManager).Assembly).WithReferences());
            ///5.连接方式
            builder.ConfigureEndpoints(System.Net.IPAddress.Loopback, 11111, 33333);
            ///6.Board
            builder.UseDashboard(options =>
            {
                options.Port = 8888;
            });
            ///7.配置Grain引用
          

        }

    }
}