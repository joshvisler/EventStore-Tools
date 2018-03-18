using System.IO;
using BackupRestoreService.Core.Interfaces;
using BackupRestoreService.Core.Services;
using BackupRestoreService.Infrastrucute.Context;
using BackupRestoreService.Infrastrucute.FileSystemManager;
using BackupRestoreService.Infrastrucute.Repositories;
using EventStoreTools.Web.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BackupRestoreService
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
            var connection = Configuration["ConnectionStrings:BackupRestoreConnectionString"];

            services.AddDbContext<RestoreBackupContext>(options =>
                options.UseSqlite(connection)
            );



            services.AddMvc();
            services.AddTransient<IBackupService, BackupService>();
            services.AddTransient<IRestoreService, RestoreService>();
            services.AddTransient<IRestoreRepository, RestoreRepository>();
            services.AddTransient<IBackupRepository, BackupRepository>();
            services.AddTransient<IBackupRestoreFileManager, BackupRestoreFileManager>();
            services.AddTransient<IFileManager, LocalFileManagment>();
            services.AddTransient<ILogger, ApplicationLogger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger("ApplicationLogger");

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<RestoreBackupContext>();
                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCors(builder =>
             builder.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());

            app.UseMvc();
        }
    }
}
