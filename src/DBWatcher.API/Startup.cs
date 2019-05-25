using AutoMapper;
using DBWatcher.Core.Execution;
using DBWatcher.Core.Services;
using DBWatcher.Infrastructure.Data;
using DBWatcher.Infrastructure.Rabbit;
using DBWatcher.Scheduling.Quartz;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DBWatcher.API
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
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
            services.AddMvc().AddNewtonsoftJson();

            services.AddAutoMapper();
            services.AddRabbitMessageBus(Configuration.GetConnectionString("Rabbit"));
            /*services.AddUnitOfWork(Configuration.GetConnectionString("Mongo"),
                UnitOfWorkEventConfigurator.AddEventHandlers);*/
            services.AddUnitOfWork(Configuration.GetConnectionString("Mongo"));
            services.AddSingleton<ICryptoManager, CryptoManager>();
            services.AddExecution();
            services.AddSingleton<IConnectionPropertiesService, ConnectionPropertiesService>();
            services.AddSingleton<IScriptService, ScriptService>();
            services.AddSingleton<IDatabaseService, DatabaseService>();
            services.AddQuartz(new QuartzProperties {
                InstanceName = "DBWatcher",
                SerializerType = "binary",
                StoreConnectionString = Configuration.GetConnectionString("QuartzStorage")
            });
            services.AddQuartzScriptScheduler();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();
        }
    }
}