using FiledPaymentProcessor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;



namespace FiledPaymentProcessor.API.Configurations
{
    public static class ConnectionConfiguration
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services,
           IConfiguration configuration)
        {
            var connection = String.Empty;

            services.AddDbContextPool<PaymentProcessorContext>(c =>
                  c.UseSqlServer(configuration.GetConnectionString("PaymentProcessorDB")));

            //services.AddSingleton(new SqlConnection(connection));

            return services;
        }
    }
}
