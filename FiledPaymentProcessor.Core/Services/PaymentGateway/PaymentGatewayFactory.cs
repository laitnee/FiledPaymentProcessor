using System;
using System.Collections.Generic;
using System.Text;

namespace FiledPaymentProcessor.Core.Services.PaymentGateway
{
    public class PaymentGatewayFactory : IPaymentGatewayFactory
    {
        public PaymentGatewayFactory()
        {

        }
        public IPaymentGateway CreateCheapGatewayCheapService() => new CheapPaymentGateway(new CheapPaymentService());
        public IPaymentGateway CreateExpensiveGatewayExpensiveService() => new ExpensivePaymentGateway(new ExpensivePaymentService());
        public IPaymentGateway CreateExpensiveGatewayPremiumService() => new ExpensivePaymentGateway(new PremiumPaymentService());
      
    }
}
