using AutoMapper;
using FluentValidation;
using MFR.Core.DTO.Validator;
using MFR.Core.Mappings;
using MFR.Core.Service;
using MFR.Core.Service.Implementation;
using MFR.DomainModels;
using MFR.Persistence.Repository;
using MFR.Persistence.Repository.Implementations;
using MFR.Persistence.UnitOfWork;
using MFR.Persistence.UnitOfWork.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MFR.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IMenuRepo, MenuRepo>();
            services.AddScoped<IOrderRepo, OrderRepo>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISubMenuRepo, SubMenuRepo>();
            services.AddScoped<IOrderDetailRepo, OrderDetailRepo>();
            services.AddScoped<IReservationRepo, ReservationRepo>();
            services.AddScoped<IShoppingBasketItemRepo, ShoppingBasketItemRepo>();
            services.AddScoped<IShoppingBasketRepo, ShoppingBasketRepo>(sb => ShoppingBasketRepo.GetBasket(sb));
        }

        public static void ConfigureAppCore(this IServiceCollection services)
        {
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ISubMenuService, SubMenuService>();
            services.AddScoped<IFileUploadService, FileUploadService>();
            services.AddScoped<IValueAddedTaxService, ValueAddedTaxService>();
            services.AddScoped<IShoppingBasketService, ShoppingBasketService>();
        }

        public static void ConfigureValidator(this IServiceCollection services)
        {
            services.AddTransient<IValidator<Menu>, MenuValidator>();
            services.AddTransient<IValidator<Order>, OrderValidator>();
            services.AddTransient<IValidator<SubMenu>, SubMenuValidator>();
        }

        public static void ConfigureAutomapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(c => config.CreateMapper());
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    policy => policy.AllowAnyOrigin().WithMethods("Get").WithHeaders("accept", "content"));
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }
    }
}
