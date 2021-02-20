using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using P_payment.Controllers;
using P_payment.Data;
using P_payment.Repository.IRepository;
using System.Net.Http;
using AutoMapper;
using P_payment.Models.Dtos;
using System;
using P_payment.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ProPayTest
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IExpensivePaymentGateway> _mockExpensivePaymentGateway;
        private Mock<ICheapPaymentGateway> _mockCheapPaymentGateway;
        private Mock<IPremiumPaymentService> _mockPremiumPaymentService;
        private Mock<IMapper> _mockMapper;
        private Mock<IHttpClientFactory> _mockClientFactory;
        private ApplicationDbContext _db;
        private ProPaymentController _paymentController;

        [TestInitialize]
        public void Initializer()
        {
            _mockExpensivePaymentGateway = new Mock<IExpensivePaymentGateway>();
            _mockCheapPaymentGateway = new Mock<ICheapPaymentGateway>();
            _mockPremiumPaymentService = new Mock<IPremiumPaymentService>();
            _mockMapper = new Mock<IMapper>();
            _mockClientFactory = new Mock<IHttpClientFactory>();
            _paymentController = new ProPaymentController(_mockCheapPaymentGateway.Object,
                _mockExpensivePaymentGateway.Object, _mockPremiumPaymentService.Object, _mockClientFactory.Object, _mockMapper.Object);
        }
        [TestMethod]
        public async Task TestMethod1()
        {
            // Arrange
            PaymentModel testItem = new PaymentModel()
            {
                CreditCardNumber = "5399415800351940",
                ExpirationDate = Convert.ToDateTime("2021-03-06T17:16:40"),
                SecurityCode = "443",
                CardHolder = "Card Holder Ones",
                Amount = Convert.ToDecimal(220)
            };
            _mockExpensivePaymentGateway.Setup(f => f.CreatePayment(testItem)).Returns((true));
            // Act
            var returnObject = await _paymentController.ProcessPayment(testItem);
            var actualResult = (StatusCodeResult)returnObject;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)actualResult.StatusCode);
        }
    }
}
