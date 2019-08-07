using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prototype.API.Dapper.Configurations;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using Prototype.API.Domain.Mapping;

namespace Prototype.API.Dapper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddResponseCaching();

            // Validators
            //services.AddValidators(); //TODO 1) Fix validation unhandled exception otherwise 2) Handle InvalidModelStateResponseFactory via data annotations
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);//.AddFluentValidation()
                    //.ConfigureApiBehaviorOptions(options =>
                    //{
                    //    // Adds a custom error response factory when ModelState is invalid
                    //    options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
                    //});

            //=== Automapper configuration
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ModelToResourceProfile());
                mc.AddProfile(new ResourceToModelProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.ConfigureSupervisor()
                .ConfigureRepositories()
                .AddMiddleware()
                .AddCorsConfiguration()
                .AddConnectionProvider(Configuration)
                .AddAppSettings(Configuration);

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info()
                {
                    Title = "Chinook Reduced API",
                    Version = "v1",
                    Description = "Chinook Reduced Music Store API"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
                //s.AddFluentValidationRules();
            });
            
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
