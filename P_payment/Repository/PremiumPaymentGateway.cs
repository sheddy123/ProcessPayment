using P_payment.Data;
using P_payment.Models;
using P_payment.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P_payment.Repository
{
    public class PremiumPaymentGateway : IPremiumPaymentService
    {
        private readonly ApplicationDbContext _db;
        public bool CreatePayment(PaymentModel paymentModel)
        {
            _db.tbl_Payment.Add(paymentModel);
            return Save();
        }
        public bool UpdatePayment(PaymentModel paymentModel)
        {
            _db.tbl_Payment.Update(paymentModel);
            return Save();
        }
        public bool DeletePayment(PaymentModel paymentModel)
        {
            _db.tbl_Payment.Remove(paymentModel);
            return Save();
        }
        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}
