using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyRestfulAPI.API.Validators;
using MyRestfulAPI.Core.Interfaces;
using MyRestfulAPI.Infrastucture.Data;
using MyRestfulAPI.Infrastucture.Dto.City;
using MyRestfulAPI.Infrastucture.Dto.Country;
using MyRestfulAPI.Infrastucture.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace MyRestfulAPI.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<IValidator<CountryAddDto>, CountryAddDtoValidator>();
            services.AddTransient<IValidator<CityUpdateDto>, CityUpdateDtoValidator>();

            
            services.AddMvc()
                    .AddJsonOptions(options=> {
                        //options.SerializerSettin 
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // swagger ui
            // other configs;
            // c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MyRestAPI", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "MyRestfulAPI.API.XML");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper();

            services.AddDbContext<DbContext>(options =>
            {
                options.UseInMemoryDatabase("MyDb");
                //options.UseLoggerFactory();
            });

            services.AddCors();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            app.UseSwaggerUI(c =>
            {
                //c.RoutePrefix = "swagger/ui";
                c.SwaggerEndpoint("v1/swagger.json", "ChatBotApi");
            });

            //app.UseCors(builder => builder.WithOrigins(""));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
