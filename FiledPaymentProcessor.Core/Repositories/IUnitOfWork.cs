using System;
using System.Collections.Generic;
using System.Text;
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
