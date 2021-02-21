using System;
using System.Collections.Generic;
using System.Text;

namespace FiledPaymentProcessor.Core.Entities
{
    public class PaymentDetail: BaseEntity
    {
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

    }
}
