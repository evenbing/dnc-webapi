using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
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
using MyRestfulAPI.Infrastucture.Extensions;
using MyRestfulAPI.Infrastucture.Repositories;
using MyRestfulAPI.Infrastucture.Services;
using Newtonsoft.Json.Serialization;
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


            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    })
                    .AddFluentValidation()
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

            //services.AddPropertyMappings
            services.AddTransient<ITypeHelperService, TypeHelperService>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper();

            services.AddTransient<IValidator<CountryAddDto>, CountryAddDtoValidator>();
            services.AddTransient<IValidator<CityUpdateDto>, CityUpdateDtoValidator>();

            services.AddDbContext<MyDbContext>(options =>
            {
                //内存数据库
                //options.UseSqlServer()
                options.UseInMemoryDatabase("mydb");
                //options.UseLoggerFactory();
            });

            services.AddCors();
            services.AddCors(options =>
            {

            });

            services.AddPropertyMappings();

            services.Configure<MvcOptions>(options =>
            {
                //options.Filters.Add(new CorsAuthorzie)
            });

            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>(options =>
            {
                options.GeneralRules = new List<RateLimitRule>()
                {
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Limit = 10,
                        Period ="5m"
                    },
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Limit = 2,
                        Period ="10s"
                    }
                };
            });

            
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
                c.SwaggerEndpoint("v1/swagger.json", "MyRestAPI");
            });

            //app.UseCors(builder => builder.WithOrigins(""));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
