using P_payment.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_payment.Models.Dtos
{
    public class PaymentModelDto
    {
        
        public int Id { get; set; }

        public string CreditCardNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string SecurityCode { get; set; }

        public string CardHolder { get; set; }

        public decimal Amount { get; set; }

        public string PaymentState { get; set; }
    }
}
