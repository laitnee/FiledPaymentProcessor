using FiledPaymentProcessor.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
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
