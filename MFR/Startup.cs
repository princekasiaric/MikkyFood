using FluentValidation.AspNetCore;
using MFR.DomainModels.Identity;
using MFR.Extensions;
using MFR.GlobalException;
using MFR.Persistence;
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

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 7;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = false;
            });

            services.ConfigureRepository();
            services.ConfigureAppCore();
            services.ConfigureValidator();
            services.ConfigureAutomapper();

            services.AddAuthentication().AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = _config["Tokens:Issuer"],
                    ValidAudience = _config["Tokens:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]))
                };
            });

            //services.AddHttpContextAccessor();
            //services.AddSession();
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

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All
            });

            app.ConfigureExceptionMiddleware(loggerFactory);
            //app.UseSession();

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
