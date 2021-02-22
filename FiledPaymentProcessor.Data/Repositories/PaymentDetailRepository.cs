using FiledPaymentProcessor.Core.Entities;
using FiledPaymentProcessor.Core.Repositories;

namespace FiledPaymentProcessor.Data.Repositories
{
    public class PaymentDetailRepository : BaseRepository<PaymentDetail>, IPaymentDetailRepository
    {
        public PaymentDetailRepository(PaymentProcessorContext paymentProcessorContext) : base(paymentProcessorContext)
        {
        }
    }
}
