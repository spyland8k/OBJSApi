using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OBJS.API.Models;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.CodeAnalysis.Options;

namespace OBJS.API
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
            // Here UserDbContext is added to application as service by AddDbContext. When initiating ApplicationDBContext are passed 
            //to define what database type, database name, authentication details if applicable.
            services.AddDbContext<ApplicationDBContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            // Get the database context and apply the migrations (azure database automatic migrations EF Codefirst)
            //var context = services.BuildServiceProvider().GetService<ApplicationDBContext>();
            //context.Database.Migrate();

            //This is a new feature in ASP.NET Core 2.2:
            //An IActionResult returning a client error status code(4xx) now returns a ProblemDetails body.
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            // Another using [JsonIgnore]; need to add the attribute to all the models, we may have the cyclic reference.
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                )
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.DateFormatString = "dd-MM-yyyy hh-mm-ss"); // hh= hours, mm= minutes, ss= seconds.

            services.AddControllers();
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
        }
    }
}
