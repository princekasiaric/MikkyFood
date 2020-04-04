using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MFR.Core.DTO.Validator;
using MFR.Core.Mappings;
using MFR.Core.Service;
using MFR.Core.Service.Implementation;
using MFR.DomainModels;
using MFR.GlobalException;
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
using Microsoft.Extensions.Logging;
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

            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISubMenuService, SubMenuService>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IValueAddedTaxService, ValueAddedTaxService>();
            services.AddScoped<IShoppingBasketService, ShoppingBasketService>();

            services.AddTransient<IValidator<Menu>, MenuValidator>();
            services.AddTransient<IValidator<Order>, OrderValidator>();
            services.AddTransient<IValidator<SubMenu>, SubMenuValidator>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(c => config.CreateMapper());

            services.AddHttpContextAccessor();
            services.AddSession();
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

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
