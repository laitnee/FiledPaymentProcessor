using FiledPaymentProcessor.Core.DTOs;
using System.Threading.Tasks;

namespace FiledPaymentProcessor.Core.Services.PaymentGateway
{
    public interface IExpensiveGatewayService
    {
        Task<PaymentResponse> Pay(PaymentRequest req);
    }
}
