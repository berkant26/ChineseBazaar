using Microsoft.AspNetCore.Mvc;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Core.Helper;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    public readonly PaymentService _paymentService;

    public PaymentController()
    {
        _paymentService = new PaymentService(); // Dependency Injection kullanabilirsin
    }

    // Ödeme işlemi için POST endpoint
    [HttpPost("payment")]
    public IActionResult Payment([FromBody] PaymentRequest paymentRequest)
    {
        if (paymentRequest == null)
        {
            return BadRequest("Payment request data is missing.");
        }

        try
        {
            var payment = _paymentService.CreatePayment(
                paymentRequest.Price,
                paymentRequest.CardHolderName,
                paymentRequest.CardNumber,
                paymentRequest.ExpireMonth,
                paymentRequest.ExpireYear,
                paymentRequest.Cvc
            );
            if (payment.Result.Status == "success")
            {
                return Ok(new { PaymentId = payment.Id, Status = "Success", PaymentStatus = payment.Result , PaymentMdStatus = payment.Result.MdStatus });
            }
            if (payment.Result.Status == "failure")
            {
                return BadRequest(new { Hata = payment.Result.ErrorMessage });

            }


            else
            {
                return BadRequest(new { Status = "Failed", Error = payment.Exception });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Status = "Error", Message = ex.Message });
        }
    }
}
public class PaymentRequest
{
    public decimal Price { get; set; }
    public string CardHolderName { get; set; }
    public string CardNumber { get; set; }
    public string ExpireMonth { get; set; }
    public string ExpireYear { get; set; }
    public string Cvc { get; set; }
}
