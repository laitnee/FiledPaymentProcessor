using FiledPaymentProcessor.Core.DTOs;
using FiledPaymentProcessor.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FiledPaymentProcessor.API.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class FiledPaymentController : Controller
    {
        private readonly ILogger<FiledPaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public FiledPaymentController(ILogger<FiledPaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        /// <summary>
        /// Charges a card based on the amount supplied
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /ProcessPayment
        ///     {
        ///        "CreditCardNumber": "4556031415183354",
        ///        "CardHolder": "Jane Doe",
        ///        "ExpirationDate": "2021-02-21",
        ///        "SecurityCode": "342",
        ///        "Amount": 23.44
        ///     }
        ///
        /// </remarks>
        /// <param name="PaymentRequest"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Returns if payment is processed</response>
        /// <response code="400">Returns if request body is invalid</response>    
        [HttpPost]
        [Route("ProcessPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest paymentRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var response = await _paymentService.ProcessPaymentRequest(paymentRequest);
            if (response != null) return Ok(response);
            return StatusCode(500);
        }
    }
}
