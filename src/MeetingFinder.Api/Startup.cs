using System;
using System.IO;
using MeetingFinder.Api.Queries.Employees;
using MeetingFinder.Api.Queries.Meetings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddTransient(GetEmployeeFileProvider)
                    .AddTransient<IEmployeeQuery, EmployeeQuery>()
                    .AddTransient<IMeetingQuery, MeetingQuery>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection()
               .UseRouting()
               .UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private Func<IEmployeeFileReader> GetEmployeeFileProvider(IServiceProvider serviceProvider)
        {
            return () => new EmployeeFileReader(() => new StreamReader(Configuration.GetValue<string>("DataFilePath")));
        }
    }
}