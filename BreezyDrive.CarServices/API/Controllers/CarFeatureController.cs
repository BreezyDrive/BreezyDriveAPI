using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.CarServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarFeatureController(ICarFeatureService carFeatureService) : BaseController
{
    [HttpGet("GetAllCarFeatures")]
    public async Task<IActionResult> GetAllCarFeatures()
    {
        return CustomResult("Success", await carFeatureService.GetAllAsync());
    }

    [HttpGet("GetCarFeatureByGuid")]
    public async Task<IActionResult> GetCarFeatureByGuid(Guid carFeatureGuid)
    {
        return CustomResult("Success", await carFeatureService.GetByGuid(carFeatureGuid));
    }

    [HttpPost("AddCarFeature")]
    public async Task<IActionResult> CreateCarFeature([FromBody] CarFeatureRequest carFeatureRequest)
    {
        return CustomResult("Success", await carFeatureService.CreateCarFeature(carFeatureRequest));
    }

    [HttpPatch("UpdateCarFeature")]
    public async Task<IActionResult> UpdateCarFeature([FromBody] CarFeatureRequest carFeatureRequest)
    {
        return CustomResult("Success", await carFeatureService.UpdateCarFeature(carFeatureRequest.Id, carFeatureRequest));
    }

    [HttpDelete("DeleteCarFeature")]
    public async Task<IActionResult> DeleteCarFeature(Guid carFeatureGuid)
    {
        return CustomResult("Success", await carFeatureService.DeleteCarFeature(carFeatureGuid));
    }
    
}