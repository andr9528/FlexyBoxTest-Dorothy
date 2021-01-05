using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wolf.Utility.Core.Startup;
using Wolf.Utility.Core.Startup.Modules;

namespace Dorothy.WebSearch
{
    /// <summary>
    /// I Inherit from my ModularStartup class in Wolf.Utility.Core.Startup;, as i have created a way to easily activate certain frameworks.
    /// In the case for this startup, i am adding a module for Swagger, in the constructor of the startup. 
    /// The Swagger module then gets called by SetupServices in ConfigureServices, and SetupApplication in Configure.
    /// This means, in my eyes, that the Startup calss supports 'Single Responsibility' much better, as the Responsibility of enableing Swagger is moved to the module.
    /// I also have similar Modules for use with Entity Framework Core and SignalR.
    /// </summary>
    public class Startup : ModularStartup
    {
        public Startup(IConfiguration configuration) : base(configuration)
        {
            AddModule(new SwaggerStartupModule());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            SetupServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            SetupApplication(app);
        }
    }
}
