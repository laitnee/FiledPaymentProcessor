using FiledPaymentProcessor.Core.DTOs;
using System.Net;
using System.Threading.Tasks;

namespace FiledPaymentProcessor.Core.Services.PaymentGateway
{
    public class CheapPaymentService
    {
        public async Task<PaymentResponse> PayCheap(PaymentRequest req)
        {
            return await Task.Run(() =>
            {
                return new PaymentResponse { ResponseCode = HttpStatusCode.NotFound, ResponseDescription = "Stubbed Service" };
            }
            );
        }
    }
}
