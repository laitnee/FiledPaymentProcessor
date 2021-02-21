using System;

namespace FiledPaymentProcessor.Core.DTOs
{
    public class PaymentResponseDTO
    {
        public Guid Id { get; set; }
        public Decimal Amount { get; set; }
        public string PaymentState { get; set; }
    }
}
