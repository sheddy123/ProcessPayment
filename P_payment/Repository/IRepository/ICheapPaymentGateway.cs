using P_payment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_payment.Repository.IRepository
{
    public interface ICheapPaymentGateway
    {
        bool CreatePayment(PaymentModel paymentModel);
        bool UpdatePayment(PaymentModel paymentModel);
        bool DeletePayment(PaymentModel paymentModel);
        bool Save();
    }
}
