using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P_payment.Models;
using P_payment.Models.Dtos;
using P_payment.Repository.IRepository;

namespace P_payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProPaymentController : ControllerBase
    {
        private readonly ICheapPaymentGateway _cheapPaymentGateway;
        private readonly IExpensivePaymentGateway _expensivePaymentGateway;
        private readonly IPremiumPaymentService _premiumPaymentService;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;

        public ProPaymentController
            (ICheapPaymentGateway cheapPaymentGateway,
            IExpensivePaymentGateway expensivePaymentGateway,
            IPremiumPaymentService premiumPaymentService,
            IHttpClientFactory clientFactory,
            IMapper mapper)
        {
            _cheapPaymentGateway = cheapPaymentGateway;
            _expensivePaymentGateway = expensivePaymentGateway;
            _premiumPaymentService = premiumPaymentService;
            _mapper = mapper;
            _clientFactory = clientFactory;
        }

        

        #region End Point to Process Payment
        /// <summary>
        /// End point to Process Payment
        /// </summary>
        /// <param name="paymentModel"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(PaymentModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment(PaymentModel paymentModel)
        {
            if (paymentModel == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var paymentModel = _mapper.Map<PaymentModel>(paymentModelDto);
            try
            {
                //If the amount to be paid is less than £20, use ICheapPaymentGateway
                if (paymentModel.Amount < 20)
                {
                    if (!_cheapPaymentGateway.CreatePayment(paymentModel))
                    {
                        ModelState.AddModelError("", $"Could not process payment for cardholder {paymentModel.CardHolder} please try again..");
                        return StatusCode(500, ModelState);
                    }
                    return StatusCode(200);
                }

                //If the amount to be paid is £21-500, use IExpensivePaymentGateway if available
                if (paymentModel.Amount > 20 && paymentModel.Amount < 501)
                {
                    if (!_expensivePaymentGateway.CreatePayment(paymentModel))
                    {
                        paymentModel.PaymentState = "pending";
                        var response = await _expensivePaymentGateway.PostMessage(paymentModel, "ExpensivePayment");
                        if (response.Equals("OK"))
                        {
                            paymentModel.PaymentState = "Processed";
                            return StatusCode(200);
                        }
                        paymentModel.PaymentState = "failed";
                        ModelState.AddModelError("", $"Could not process payment for cardholder {paymentModel.CardHolder} please try again..");
                        return StatusCode(500, ModelState);
                    }
                    return StatusCode(200);
                }

                //If the amount is > £500, try only PremiumPaymentService and retry up to 3 times in case paymentdoes not get processed
                if (paymentModel.Amount > 500)
                {
                    if (!_premiumPaymentService.CreatePayment(paymentModel))
                    {
                        paymentModel.PaymentState = "pending";
                        var response = await _premiumPaymentService.PostMessage(paymentModel, "ExpensivePayment");
                        if (response.Equals("OK"))
                        {
                            paymentModel.PaymentState = "Processed";
                            return StatusCode(200);
                        }
                        paymentModel.PaymentState = "failed";
                        ModelState.AddModelError("", $"Could not process payment for cardholder {paymentModel.CardHolder} please try again..");
                        return StatusCode(500, ModelState);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest();


        }
        #endregion
    }
}