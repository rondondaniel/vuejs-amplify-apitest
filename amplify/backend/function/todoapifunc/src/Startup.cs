using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using todoapifunc.Models;

namespace todoapifunc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // DBContext
            services
                .AddDbContext<TodoDbContext>(options => options
                    // To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                    .UseMySql(
                        Configuration.GetValue<string>("APP_DATABASE_CONNECTION_STRING"),
                        mysqlOptions =>  mysqlOptions
                            .ServerVersion(new ServerVersion(new Version(10, 4, 21), ServerType.MariaDb)))
                );
  
            // Controllers
            services.AddControllers();

            // CORS setup
            services.AddCors(options =>
            {
                options.AddPolicy(name: Common.POLICY_API_FOR_VUEJS,
                    builder => {
                        //Dev policy
                        builder.WithOrigins("https://localhost:8080","http://localhost:8080","http://localhost:5001");
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(Common.POLICY_API_FOR_VUEJS);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("api/v2/health", async context =>
                {
                    await context.Response.WriteAsync("ASP.NET Core on AWS Lambda: Running");
                });
            });
        }
    }
}
