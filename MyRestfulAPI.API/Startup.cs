using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyRestfulAPI.API.Validators;
using MyRestfulAPI.Infrastucture.Dto.Country;
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
            
            services.AddMvc()
                    .AddJsonOptions(options=> {
                        //options.SerializerSettin 
                    })
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //swagger ui
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MyRestAPI", Version = "v1" });
                
                //c.IncludeXmlComments
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

            //app.UseCors(builder => builder.WithOrigins(""));

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
