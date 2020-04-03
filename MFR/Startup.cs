using AutoMapper;
using FluentValidation.AspNetCore;
using MFR.Core.Mappings;
using MFR.Persistence;
using MFR.Persistence.Repository;
using MFR.Persistence.Repository.Implementations;
using MFR.Persistence.UnitOfWork;
using MFR.Persistence.UnitOfWork.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

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
            var connectionString = _config["ConnectionString:Default"];
            services.AddDbContext<MFRDbContext>(option => option.UseSqlServer(connectionString));
            services.AddControllers()
                    .AddFluentValidation()
                    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling 
                                               = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddScoped<IMenuRepo, MenuRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISubMenuRepo, SubMenuRepo>();
            services.AddScoped<IOrderDetailRepo, OrderDetailRepo>();
            services.AddScoped<IReservationRepo, ReservationRepo>();
            services.AddScoped<IShoppingBasketItemRepo, ShoppingBasketItemRepo>();
            services.AddScoped<IShoppingBasketRepo, ShoppingBasketRepo>(sb => ShoppingBasketRepo.GetBasket(sb));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(c => config.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            }); ;

            app.UseRouting();

            app.UseAuthorization(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
