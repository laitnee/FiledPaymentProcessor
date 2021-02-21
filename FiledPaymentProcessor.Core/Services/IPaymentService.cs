using FiledPaymentProcessor.Core.DTOs;
using System.Threading.Tasks;

namespace FiledPaymentProcessor.Core.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponseDTO> ProcessPaymentRequest(PaymentRequest req);
    }
}
