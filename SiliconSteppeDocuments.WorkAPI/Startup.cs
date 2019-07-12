using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SiliconSteppeDocuments.DB;
using SiliconSteppeDocuments.Model;
using System.Text;

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
            SetSteppeContextOptions(services);

            services.AddIdentity<ApplicationUser, ApplicationRole>(
                option =>
                {
                    option.User.RequireUniqueEmail = true;
                    option.Password.RequireDigit = false;
                    option.Password.RequiredLength = 5;
                    option.Password.RequireNonAlphanumeric = false;
                    option.Password.RequireUppercase = false;
                    option.Password.RequireLowercase = false;
                }
            )
            .AddEntityFrameworkStores<SteppeContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidAudience = Configuration["Jwt:Site"],
                    ValidIssuer = Configuration["Jwt:Site"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SigningKey"])),
                    ClockSkew = System.TimeSpan.Zero
                };
            });

            // Add framework services.
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

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            SteppeInitializer.Initialize(context);
        }

        private void SetSteppeContextOptions(IServiceCollection services)
        {
            var connectionStr = Configuration.GetConnectionString("DefaultConnection");
            var sqltype = Configuration.GetSection("appSettings").GetValue<int>("SQlType");

            switch (sqltype)
            {
                case 0:
                    services.AddDbContext<SteppeContext>(options =>
                            options.UseSqlServer(connectionStr));
                    break;
                case 1:
                    services.AddDbContext<SteppeContext>(options =>
                            options.UseNpgsql(connectionStr));
                    break;
                default:
                    services.AddDbContext<SteppeContext>(options =>
                            options.UseSqlServer(connectionStr));
                    break;
            }
        }
    }
}