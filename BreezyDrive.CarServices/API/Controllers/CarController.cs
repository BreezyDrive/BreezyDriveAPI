using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.CarServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : BaseController
{
    
    [HttpGet("ahihi")]
    public IActionResult ahihi()
    {
        var result = "ahjiihih";
        return CustomResult("AHihi");
    }
    
    
    
}