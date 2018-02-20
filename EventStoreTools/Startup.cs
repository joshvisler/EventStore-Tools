using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.Services;
using EventStoreTools.Infrastructure.DataBase.Contexts;
using EventStoreTools.Infrastructure.DataBase.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using EventStoreTools.Core.Containers;
using Microsoft.Extensions.Logging;
using EventStoreTools.Web.Logger;
using System.IO;

namespace EventStoreTools
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
            var connectionString = Configuration.GetConnectionString("estoolsdb");
                services.AddEntityFrameworkNpgsql().AddDbContext<EventStoreToolsDBContext>(options => options.UseNpgsql(connectionString));
            services.AddAutoMapper(x => x.AddProfile(new MapperProfile()));
            services.AddMvc();
            services.AddTransient<IClientService, ClientService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IConnectionService, ConnectionService>();
            services.AddTransient<IConnectionRepository, ConnectionRepository>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger("ApplicationLogger");

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
