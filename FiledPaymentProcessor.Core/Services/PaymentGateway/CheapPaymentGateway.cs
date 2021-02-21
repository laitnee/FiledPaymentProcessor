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
    public class CheapPaymentGateway : ICheapPaymentGateway
    {
        private readonly CheapPaymentService _cpService;
        public CheapPaymentGateway(CheapPaymentService cpService)
        {
            _cpService = cpService;
    
        }
        public async Task<bool> PaymentProcessor(PaymentRequest paymentRequest)
        {
            try
            {
                Log.Logger.Information($"Cheap Pament Gateway:: {typeof(CheapPaymentService).Name}");

                var response = await _cpService.PayCheap(paymentRequest);

                Log.Logger.Information($"Cheap Payment Gateway response {JsonConvert.SerializeObject(response)}");

                if (response != null && response.ResponseCode == System.Net.HttpStatusCode.OK) return ResultConstants.SUCCESS;
                return ResultConstants.FAILED;
            }
            catch (Exception ex)
            {
                Log.Error($"Exception Occured::: {ex}");
            }
            return ResultConstants.FAILED;
        }
    }
}
