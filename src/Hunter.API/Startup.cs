using Hunter.API.Models;
using Hunter.DataBase;
using Hunter.DataBase.Interfaces;
using Hunter.DataBase.Repository;
using Hunter.DataBase.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.API
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; private set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            if (!env.IsDevelopment())
            {
                builder.AddJsonFile("appsettings.json", optional: false);
            }
            else
            {
                builder.AddJsonFile("appsettings.Development.json", optional: false);
            }

            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            IConfiguration config = Configuration.GetSection("HunterConfiguration");
            services.Configure<HunterConfiguration>(config);
            HunterConfiguration hunterConfiguration = config.Get<HunterConfiguration>();

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddEntityFrameworkSqlServer().AddDbContext<HunterDBContext>(options =>
            {
                options.UseSqlServer(hunterConfiguration.CONNECTION);
            });
            services.AddScoped<IHunterDBContext, HunterDBContext>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                        var userId = Guid.Parse(context.Principal.Identity.Name);
                        var user = userService.RetrieveByPK(userId);
                        
                        if (user == null)
                        {
                            context.Fail("Unauthorised");
                        }

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {

                        context.Fail("FAILED");
                        context.HttpContext.Response.StatusCode = 500;
                        return Task.CompletedTask;
                    }                    
                    
                };
                var key = Encoding.ASCII.GetBytes(hunterConfiguration.SECRET);
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;               
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,    
     
                };
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
                options.AllowAnyHeader();
                options.AllowCredentials();                
            });

            app.UseMvc(routes =>
            {
                routes.MapAreaRoute(
                name: "API",
                areaName: "API",
                template: "api/{controller=Devices}/{action=Get}/{id?}"
              );
            });
        


        }

    }
}
