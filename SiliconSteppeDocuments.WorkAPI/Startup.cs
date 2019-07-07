using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiliconSteppeDocuments.DB;

namespace SiliconSteppeDocuments.WorkAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var constr = Configuration.GetConnectionString("DefaultConnection");
            var sqltype = Configuration.GetSection("appSettings").GetValue<int>("SQlType");

            switch (sqltype)
            {
                case 0:
                    services.AddDbContext<SteppeContext>(options =>
                            options.UseSqlServer(constr));
                    break;
                case 1:
                    services.AddDbContext<SteppeContext>(options =>
                            options.UseNpgsql(constr));
                    break;
                default:
                    services.AddDbContext<SteppeContext>(options =>
                            options.UseSqlServer(constr));
                    break;
            }

            // Add framework services.
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddMvc();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SteppeContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseMvc();

            SteppeInitializer.Initialize(context);
        }
    }
}