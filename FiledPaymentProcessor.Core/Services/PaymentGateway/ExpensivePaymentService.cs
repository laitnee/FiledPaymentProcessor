using FiledPaymentProcessor.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FiledPaymentProcessor.Core.Services.PaymentGateway
{
    public class ExpensivePaymentService: IExpensiveGatewayService
    {
        public async Task<PaymentResponse> Pay(PaymentRequest req)
        {
            return await Task.Run(() =>
            {
                return new PaymentResponse { ResponseCode = HttpStatusCode.NotFound, ResponseDescription = "Stubbed Service" };
            }
            );
        }
    }
}
