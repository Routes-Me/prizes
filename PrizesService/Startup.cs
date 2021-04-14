using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PrizesService.Abstraction;
using PrizesService.DataAccess.Abstraction;
using PrizesService.DataAccess.Repository;
using PrizesService.Models.Common;
using PrizesService.Models.DBModels;
using PrizesService.Repository;

namespace PrizesService
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
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddDbContext<prizesserviceContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSingleton<Func<prizesserviceContext>>(() =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<prizesserviceContext>();
                optionsBuilder.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
                var dbContext = new prizesserviceContext(optionsBuilder.Options);
                dbContext.Database.SetCommandTimeout(TimeSpan.FromSeconds(300));
                return dbContext;
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            var dependenciessSection = Configuration.GetSection("Dependencies");
            services.Configure<Dependencies>(dependenciessSection);

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            //Common Services
            services.AddScoped<ICandidatesRepository, CandidatesRepository>();
            services.AddScoped<IDrawsRepository, DrawsRepository>();
            services.AddScoped<INationalitiesRepository, NationalitiesRepository>();
            services.AddScoped<ISpinnersRepository, SpinnersRepository>();

            //Data Access Services
            services.AddScoped<ICandidatesDataAccessRepository, CandidatesDataAccessRepository>();
            services.AddScoped<IDrawsDataAccessRepository, DrawsDataAccessRepository>();
            services.AddScoped<INationalitiesDataAccessRepository, NationalitiesDataAccessRepository>();
            services.AddScoped<ISpinnersDataAccessRepository, SpinnersDataAccessRepository>();
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
