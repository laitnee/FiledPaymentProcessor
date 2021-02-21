using System;
using System.Threading.Tasks;

namespace FiledPaymentProcessor.Core.Repositories
{

    public interface IUnitOfWork : IDisposable
    {

        IPaymentDetailRepository Payments { get; }
        IPaymentStatusRepository PaymentState { get; }
        Task<bool> Complete();
    }
}
