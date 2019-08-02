using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prototype.API.Dapper.Configurations;
using Swashbuckle.AspNetCore.Swagger;

namespace Prototype.API.Dapper
{
    public class GenreValidator
    {
        public GenreValidator(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddResponseCaching();
            services.AddMvc()//.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<GenreValidator>())
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.ConfigureSupervisor()
                .ConfigureRepositories()
                .AddMiddleware()
                .AddCorsConfiguration()
                .AddConnectionProvider(Configuration)
                .AddAppSettings(Configuration);

            services.AddSwaggerGen(s => s.SwaggerDoc("v1", new Info
            {
                Title = "Chinook Reduced API",
                Description = "Chinook Small Music Store API"
            }));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            var content = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddFile(content+ "/Log.log");
            app.UseCors("AllowAll");
            app.UseStaticFiles();
            app.UseMvc(
                routes => routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}"));
            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 docs"));
        }
    }
}
