using System.Net;

namespace FiledPaymentProcessor.Core.Services.PaymentGateway
{
    public class PaymentResponse
    {
        public HttpStatusCode ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }
}