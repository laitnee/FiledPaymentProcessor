using FiledPaymentProcessor.Core.DTOs;
using System.Threading.Tasks;

namespace FiledPaymentProcessor.Core.Services.PaymentGateway
{
    public interface IPaymentGateway
    {
        Task<bool> PaymentProcessor(PaymentRequest paymentRequest);
    }
}
