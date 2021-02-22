using FiledPaymentProcessor.Core.DTOs;
using Serilog;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FiledPaymentProcessor.Core.Services.PaymentGateway
{
    public class PremiumPaymentService : IExpensiveGatewayService
    {
        private const int RETRY_TIME_LAPSE = 500;
        private const int NO_OF_RETRIES = 4;

        public async Task<PaymentResponse> Pay(PaymentRequest req)
        {
            var result = await Task.Run(() =>
              {
                  PaymentResponse response = null;
                  int retry = 1;
                  while (retry <= NO_OF_RETRIES)
                  {
                      Log.Logger.Information($"Premium Service Payment retry {retry}");

                      response = CallPayService(req);

                      Log.Logger.Information($"Premium Service Payment response {response}");

                      if (response != null && response.ResponseCode == System.Net.HttpStatusCode.OK) return response;
                      retry++;
                      Thread.Sleep(RETRY_TIME_LAPSE * retry);
                  }
                  return response;
              }
            );
            return result;
        }

        private PaymentResponse CallPayService(PaymentRequest paymentRequest) =>
         new PaymentResponse { ResponseCode = HttpStatusCode.NotFound, ResponseDescription = "Stubbed Service" };

    }
}
