using System;
using System.Collections.Generic;
using System.Text;

namespace FiledPaymentProcessor.Core.Services.PaymentGateway
{
    public interface IPaymentGatewayFactory
    {
        IPaymentGateway CreateCheapGatewayCheapService();
        IPaymentGateway CreateExpensiveGatewayExpensiveService();
        IPaymentGateway CreateExpensiveGatewayPremiumService();
    }
}
