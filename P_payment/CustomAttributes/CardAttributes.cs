using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace P_payment.CustomAttributes
{
    public class CreditCardNumAttributes : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                //Build your Regular Expression
                Regex expression = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");
                var creditCardNumber = (string)value;
                var validCredit = expression.IsMatch(creditCardNumber);

                if (validCredit == true)
                    return ValidationResult.Success;
                else
                    return new ValidationResult("Invalid Credit Card");
            }
            catch (Exception)
            {
                return new ValidationResult("Error Processing request.");
            }
        }

    }
    public class CardExpDateAttributes : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            try
            {
                var dateVal = (DateTime)value;
                DateTime date;
                if (DateTime.TryParse(Convert.ToString(dateVal), out date))
                    if (date >= DateTime.Now)
                        return ValidationResult.Success;
                    else
                        return new ValidationResult("Card expired.");
                else
                    return new ValidationResult("Error processing request..");
            }
            catch (Exception ex)
            {
                return new ValidationResult("Not a number.");
            }
        }

    }

}
