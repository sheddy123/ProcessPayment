using AutoMapper;
using P_payment.Models;
using P_payment.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_payment.PaymentMapper
{
    public class PaymentMappings : Profile
    {
        public PaymentMappings()
        {
            CreateMap<PaymentModel, PaymentModelDto>().ReverseMap();
        }
    }
}
