using FluentValidation.AspNetCore;
using MFR.DomainModels.Identity;
using MFR.Extensions;
using MFR.GlobalException;
using MFR.Persistence;
using MFR.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;

namespace MFR
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddFluentValidation()
                    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling
                                               = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<MFRDbContext>(option => option.UseSqlServer(_config["ConnectionString:Default"]));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<MFRDbContext>().AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(option => option.TokenLifespan = TimeSpan.FromHours(24));

            var configSection = _config.GetSection("AppSettings");
            services.Configure<AppSettings>(configSection);
            var settings = configSection.Get<AppSettings>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;
            });

            services.AddAuthentication().AddFacebook(option =>
            {
                option.AppId = _config["FacebookAppId"];
                option.AppSecret = _config["FacebookAppSecret"];
            });

            services.ConfigureRepository();
            services.ConfigureAppCore();
            services.ConfigureValidator();
            services.ConfigureAutomapper();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = settings.Tokens.Issuer,
                    ValidAudience = settings.Tokens.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecretKey:Key"]))
                };
            });
            services.AddSwaggerGen();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.ConfigureExceptionMiddleware(loggerFactory);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MikkyFood Restaurant Service API V1");
            });
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
