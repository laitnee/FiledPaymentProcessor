using FiledPaymentProcessor.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace FiledPaymentProcessor.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PaymentProcessorContext _context;


        public IPaymentDetailRepository Payments { get; }

        public IPaymentStatusRepository PaymentState { get; }

        public UnitOfWork(PaymentProcessorContext paymentProcessorContext,
            IPaymentDetailRepository payment,
            IPaymentStatusRepository paymentStatus)
        {
            this._context = paymentProcessorContext;
            this.Payments = payment;
            this.PaymentState = paymentStatus;
        }
        public async Task<bool> Complete()
        {
            return Convert.ToBoolean(await _context.SaveChangesAsync());
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
