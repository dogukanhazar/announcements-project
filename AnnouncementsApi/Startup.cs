using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AnnouncementsApi.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AnnouncementsApi
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

            services.AddControllers();

            services.AddDbContext<AnnouncementContext>(options =>
            {
                options.UseInMemoryDatabase("Announcements");
            });

            services
                .AddControllersWithViews()
                .AddNewtonsoftJson();

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 5001;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("DefaultCors",
                builder => builder
                .WithOrigins("http://localhost:5000", "https://localhost:5001")
                .WithMethods("GET", "POST", "PUT", "DELETE"));

                options.AddPolicy("AllowOriginCors",
                 builder => builder.AllowAnyOrigin());
            });

            services.AddApiVersioning(v =>
            {
                v.DefaultApiVersion = new ApiVersion(1, 0);
                v.AssumeDefaultVersionWhenUnspecified = true;
                v.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(v =>
            {
                v.GroupNameFormat = "'v'VVV";
                v.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    foreach (var desc in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json",
                            desc.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseCors("AllowOriginCors");

            app.UseApiVersioning();

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
