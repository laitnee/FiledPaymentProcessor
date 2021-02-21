using AutoMapper;
using FiledPaymentProcessor.Core.DTOs;
using FiledPaymentProcessor.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiledPaymentProcessor.Core.MappingProfiles
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<PaymentRequest, PaymentDetail>();
            CreateMap<PaymentDetail, PaymentResponseDTO>().ForMember(dest => dest.PaymentState, opt => opt.MapFrom(src => src.Status.State));
        }
    }
}
