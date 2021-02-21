using FiledPaymentProcessor.Core.Entities;
using FiledPaymentProcessor.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiledPaymentProcessor.Data.Repositories
{
    public class PaymentDetailRepository : BaseRepository<PaymentDetail>, IPaymentDetailRepository
    {
        public PaymentDetailRepository(PaymentProcessorContext paymentProcessorContext) : base(paymentProcessorContext)
        {
        }
    }
}
