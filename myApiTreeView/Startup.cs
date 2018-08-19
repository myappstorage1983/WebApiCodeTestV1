using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myApiTreeView.API.Data;
using myApiTreeView.API.Utilities;
using myApiTreeView.DataSeed;
using myApiTreeView.Services;
using myApiTreeView.Utilities;
using Swashbuckle.AspNetCore.Swagger;
using System.Net;

namespace myApiTreeView
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
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
         
            services.AddMvc().AddJsonOptions(opt => {
                opt.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Dependency Injection of DataRep,Folder and Services.
            services.AddScoped<IDataRepo,DataRepo>();
            services.AddScoped<IFolderService,FolderService>();
            services.AddScoped<ITestCaseService,TestCaseService>();

            //Seeding 
            services.AddTransient<Seed>();

            //Enabling Cross Origin resource sharing.
            services.AddCors();

            //Swagger compatibility.
           services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Test Case Web API",
                    Description = "This TestCase Web  Api is used to manage the testcase files in the folders that represent TreeView Structure."
                });
            });

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfiles>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,Seed seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else 
            {
                //Global error handling in Production environment.
                //Extension method AddApplicationError will be called to faciitate the error handling.
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message);
                       
                    });
                });
            }

             app.UseSwagger();
             app.UseSwaggerUI(c =>
             {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My TestCase API V1");
             });

            //SeedFolders will seed data for all the folders at the first instance.
             seeder.SeedFolders();

            app.UseStatusCodePagesWithReExecute("/Errors/Index", "?statusCode={0}");
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());//Cors enabling for headers/Origin/Method/Credentials
            app.UseMvc();
        }
    }
}
