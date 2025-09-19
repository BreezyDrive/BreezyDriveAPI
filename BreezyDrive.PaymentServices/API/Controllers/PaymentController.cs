using BreezyDrive.PaymentServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.PaymentServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController(
    IVnpayService vnpayService) : BaseController
{

    [HttpGet]
    public Task<IActionResult> GetPaymentLink()
    {
        return null;
    }
}