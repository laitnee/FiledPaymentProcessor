using FiledPaymentProcessor.Core.Entities;
using FiledPaymentProcessor.Core.Repositories;

namespace FiledPaymentProcessor.Data.Repositories
{
    public class PaymentStatusRepository : BaseRepository<PaymentStatus>, IPaymentStatusRepository
    {
        public PaymentStatusRepository(PaymentProcessorContext paymentProcessorContext) : base(paymentProcessorContext)
        {
        }
    }
}
