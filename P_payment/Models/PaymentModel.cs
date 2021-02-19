using P_payment.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_payment.Models
{
    public class PaymentModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [CreditCardNumAttributes]
        public string CreditCardNumber { get; set; }
        
        [Required]
        [CardExpDateAttributes]
        public DateTime  ExpirationDate { get; set; }
        
        
        [StringLength(3, ErrorMessage = "Invalid Security Code", MinimumLength = 3)]
        public string SecurityCode { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required(ErrorMessage = "The field Amount is required and must be between 1 and 10000000000000000.")]
        [Range(1, 9999999999999999.99)]
        public decimal Amount { get; set; }

        public string PaymentState { get; set; }
    }
}
