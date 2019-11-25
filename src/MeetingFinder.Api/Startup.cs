using System;
using System.IO;
using FluentValidation;
using MeetingFinder.Api.Queries.Employees;
using MeetingFinder.Api.Queries.Meetings;
using MeetingFinder.Api.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace MeetingFinder.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeetingFinder API", Version = "v1" });
            });
            services.AddTransient(GetEmployeeFileProvider)
                    .AddTransient<IEmployeeQuery, EmployeeQuery>()
                    .AddTransient<IMeetingQuery, MeetingQuery>()
                    .AddTransient<IValidator<SuitableMeetingsRequest>, SuitableMeetingsRequestValidator>();

        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection()
                .UseRouting()
                .UseEndpoints(endpoints => { endpoints.MapControllers(); })
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeetingFinder API v1");
                });
        }

        private Func<IEmployeeFileReader> GetEmployeeFileProvider(IServiceProvider serviceProvider)
        {
            return () => new EmployeeFileReader(() => new StreamReader(Configuration.GetValue<string>("DataFilePath")));
        }
    }
}