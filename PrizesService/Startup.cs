using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidAudience,
                    ValidIssuer = appSettings.ValidIssuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret)),
                    // verify signature to avoid tampering
                    ValidateLifetime = true, // validate the expiration
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.FromMinutes(30) // tolerance for the expiration date
                };
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        logger.LogError("Authentication failed.", context.Exception);
                        context.Response.StatusCode = 401;
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        context.Response.OnStarting(async () =>
                        {
                            context.Response.ContentType = "application/json";
                            ErrorMessage errorMessage = new ErrorMessage();
                            errorMessage.Message = "Authentication failed.";
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorMessage));
                        });
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                        logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                        return Task.CompletedTask;
                    }
                };
            });

            //Common Services
            services.AddScoped<ICandidatesRepository, CandidatesRepository>();
            services.AddScoped<IDrawsRepository, DrawsRepository>();
            services.AddScoped<INationalitiesRepository, NationalitiesRepository>();
            services.AddScoped<IObfuscationRepository, ObfuscationRepository>();

            //Data Access Services
            services.AddScoped<ICandidatesDataAccessRepository, CandidatesDataAccessRepository>();
            services.AddScoped<IDrawsDataAccessRepository, DrawsDataAccessRepository>();
            services.AddScoped<INationalitiesDataAccessRepository, NationalitiesDataAccessRepository>();
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
