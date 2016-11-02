using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LocalSocial.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LocalSocial
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build().ReloadOnChanged("appsettings.json");
        }
        public IConfigurationRoot Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddApplicationInsightsTelemetry(Configuration);
            //services.AddEntityFramework()
            //    .AddSqlServer()
            //    .AddDbContext<LocalSocialContext>(options =>
            //            options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
            //services.AddScoped(provicer =>
            //{
            //    return new LocalSocialContext();
            //});
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<LocalSocialContext>()
                .AddDefaultTokenProviders();
            
            
            services.AddMvc();
        }

        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            

            app.UseIISPlatformHandler();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc();
        }
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
        // Entry point for the application.
    }
}
