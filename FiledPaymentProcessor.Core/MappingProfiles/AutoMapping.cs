using AutoMapper;
using FiledPaymentProcessor.Core.DTOs;
using FiledPaymentProcessor.Core.Entities;

namespace FiledPaymentProcessor.Core.MappingProfiles
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<PaymentRequest, PaymentDetail>();
            CreateMap<PaymentDetail, PaymentResponseDTO>().ForMember(dest => dest.PaymentState, opt => opt.MapFrom(src => src.Status.State));
        }
    }
}
