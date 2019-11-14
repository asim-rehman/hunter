using Hunter.Web.Client.Models;
using Hunter.Web.Client.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hunter.Web.Client
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; private set; }
        public Startup(IWebHostEnvironment env)
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

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.Configure<HunterConfiguration>(Configuration.GetSection("HunterConfiguration"));            
            services.AddSingleton<DevicesService>();
            services.AddSingleton<TasksService>();
            services.AddSingleton<TasksDataService>();
            services.AddSingleton<UsersService>();
            services.AddSingleton<BaseService>();
            services.AddScoped<AppState>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
