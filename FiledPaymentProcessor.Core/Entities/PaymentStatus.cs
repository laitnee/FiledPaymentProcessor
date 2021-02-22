using System;

namespace FiledPaymentProcessor.Core.Entities
{
    public class PaymentStatus : BaseEntity
    {
        public PaymentState State { get; set; }

        public Guid PaymentDetailId { get; set; }
        public PaymentDetail PaymentDetail { get; set; }
    }
    public enum PaymentState { PENDING, PROCESSED, FAILED }
}
