namespace EasyRepository.Sample;

using System;
using System.IO;
using System.Reflection;
using AutoFilterer.Swagger;
using Context;
using EFCore.Generic;
using MarkdownDocumenting.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseSwagger();

        app.UseSwaggerUI(
            options =>
            {
                options.EnableDeepLinking();
                options.ShowExtensions();
                options.DisplayRequestDuration();
                options.DocExpansion(DocExpansion.None);
                options.RoutePrefix = "api-docs";
                options.SwaggerEndpoint("/swagger/EasyRepository/swagger.json", "EasyProfilerSwagger");
            });
        app.UseDocumentation(opts => this.Configuration.Bind("DocumentationOptions", opts));
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddDbContext<SampleDbContext>(options => { options.UseNpgsql(this.Configuration.GetConnectionString("DefaultConnection")); }, ServiceLifetime.Transient);

        services.ApplyEasyRepository<SampleDbContext>();

        services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc(
                    "EasyRepository",
                    new OpenApiInfo
                    {
                        Title = "Easy Repository",
                        Version = "1.0.0",
                        Description = "This repo, provides implement generic repository pattern on ef core",
                        Contact = new OpenApiContact
                        {
                            Email = "furkan.dvlp@gmail.com",
                            Url = new Uri("https://github.com/furkandeveloper/EasyRepository.EFCore")
                        }
                    });
                options.UseAutoFiltererParameters();
                var docFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, docFile);

                if (File.Exists(filePath))
                {
                    options.IncludeXmlComments(filePath);
                }

                options.DescribeAllParametersInCamelCase();
            });
        services.AddDocumentation();
    }
}