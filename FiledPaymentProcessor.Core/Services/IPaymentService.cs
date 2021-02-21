using FiledPaymentProcessor.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FiledPaymentProcessor.Core.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponseDTO> ProcessPaymentRequest(PaymentRequest req);
    }
}
