using FiledPaymentProcessor.Core.MappingProfiles;
using FiledPaymentProcessor.Core.Repositories;
using FiledPaymentProcessor.Core.Services;
using FiledPaymentProcessor.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FiledPaymentProcessor.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<IPaymentDetailRepository, PaymentDetailRepository>();
            services.AddTransient<IPaymentStatusRepository, PaymentStatusRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IPaymentService, PaymentService>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup), typeof(AutoMapping));
        }

        public static void AddCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
        }
    }
}
