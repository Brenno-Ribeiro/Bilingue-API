using Bilingue.Application.AutoMapper;
using Bilingue.Application.Intefaces;
using Bilingue.Application.Services;
using Bilingue.Domain.DomainClassroom.Repository;
using Bilingue.Domain.DomainRegistraition.Repository;
using Bilingue.Domain.DomainStudent.Repository;
using Bilingue.Infra.Context;
using Bilingue.Infra.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Bilibgue.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            ConfigureSwaggerGenService(services);
            ConfigureAutoMapper(services);
            ConfigureDbContext(services);
            ConfigureDependencyInjection(services);
        }


        public void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<IStudentRespository, StudentRepository>();
            services.AddScoped<IClassroomRepository, ClassroomRepository>();
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();

            services.AddScoped<IStudentAppService, StudentAppService>();
            services.AddScoped<IClassroomAppService, ClassroomAppService>();
            services.AddScoped<IRegistrationAppService, RegistrationAppService>();
        }

        public void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<BilingueContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Bilingue"));

            });
        }


        public void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DomainToViewModel));
            services.AddAutoMapper(typeof(ViewModelToDomain));
        }



        public void ConfigureSwaggerGenService(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bilingue", Version = "v1" });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.DescribeAllParametersInCamelCase();

            });
        }




        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bilibgue.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
