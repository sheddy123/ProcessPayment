using Newtonsoft.Json;
using P_payment.Data;
using P_payment.Models;
using P_payment.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P_payment.Repository
{
    public class ExpensivePaymentGateway : IExpensivePaymentGateway
    {

        private readonly ApplicationDbContext _db;

        private readonly IHttpClientFactory _clientFactory;
        
        public ExpensivePaymentGateway(ApplicationDbContext db, IHttpClientFactory clientFactory)
        {
            _db = db;
            _clientFactory = clientFactory;
        }
        public bool CreatePayment(PaymentModel paymentModel)
        {
            try
            {
                paymentModel.PaymentState = "Processed";
                _db.tbl_Payment.Add(paymentModel);
            }
            catch(Exception ex)
            {

            }
            return Save();
        }
        public bool UpdatePayment(PaymentModel paymentModel)
        {
            _db.tbl_Payment.Update(paymentModel);
            return Save();
        }
        public async Task<string> PostMessage(PaymentModel postData, string clientType)
        {
            var httpClient = _clientFactory.CreateClient(clientType);
            var url = $"https://localhost:44391/api/ProPayment/ProcessPayment";

            using (var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json"))
            {
                var result = await httpClient.PostAsync(url, content);
                // The call was a success
                if (result.StatusCode == HttpStatusCode.Accepted)
                {
                    return "OK";
                }
                // The call was not a success, do something
                else
                {
                    return "Error";
                }
            }
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
