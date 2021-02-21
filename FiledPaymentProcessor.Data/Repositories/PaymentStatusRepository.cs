using FiledPaymentProcessor.Core.Entities;
using FiledPaymentProcessor.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiledPaymentProcessor.Data.Repositories
{
    public class PaymentStatusRepository : BaseRepository<PaymentStatus>, IPaymentStatusRepository
    {
        public PaymentStatusRepository(PaymentProcessorContext paymentProcessorContext) : base(paymentProcessorContext)
        {
        }
    }
}
