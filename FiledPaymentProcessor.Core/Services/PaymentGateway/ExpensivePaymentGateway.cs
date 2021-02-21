using FiledPaymentProcessor.Core.DTOs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FiledPaymentProcessor.Core.Services.PaymentGateway
{
    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {
        private readonly IExpensiveGatewayService _epService;
        public ExpensivePaymentGateway(IExpensiveGatewayService epService)
        {
            _epService = epService;
        }
        public async Task<bool> PaymentProcessor(PaymentRequest paymentRequest)
        {
            try
            {
                Log.Logger.Information($"Expensive Gateway:: {typeof(CheapPaymentService).Name}");
                var response = await _epService.Pay(paymentRequest);
                Log.Logger.Information($"Expensive Payment Gateway response  {JsonConvert.SerializeObject(response)}");

                if (response != null && response.ResponseCode == System.Net.HttpStatusCode.OK) return ResultConstants.SUCCESS;
                if (_epService is ExpensivePaymentService)
                {
                    var retryResult = await RetryPayment(paymentRequest);
                    if(retryResult == ResultConstants.SUCCESS) return ResultConstants.SUCCESS;
                }
                return ResultConstants.FAILED;
            }
            catch (Exception ex)
            {
                Log.Error($"Exception Occured::: {ex}");
            }
            return ResultConstants.FAILED;
        }

        private async Task<bool> RetryPayment(PaymentRequest paymentRequest)
        {
            var cheapPaymentGateway = new CheapPaymentGateway(new CheapPaymentService());
            return await cheapPaymentGateway.PaymentProcessor(paymentRequest);
        }
    }
}
