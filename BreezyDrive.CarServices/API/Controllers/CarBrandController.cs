using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.CarServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarBrandController(ICarBrandService carBrandService) : BaseController
{
    [HttpGet("GetAllFeatures")]
    public async Task<IActionResult> GetAllFeatures()
    {
        return CustomResult("Success", await carBrandService.GetAllAsync());
    }

    [HttpGet("GetFeatureByGuid")]
    public async Task<IActionResult> GetFeatureByGuid(Guid carBrandId)
    {
        return CustomResult("Success", await carBrandService.GetByGuid(carBrandId));
    }

    [HttpPost("CreateFeature")]
    public async Task<IActionResult> CreateFeature([FromBody]CarBrandRequest carBrandController)
    {
        return CustomResult("Success", await carBrandService.CreateCarBrand(carBrandController));
    }

    [HttpPatch("UpdateFeature/{featureId}")]
    public async Task<IActionResult> UpdateFeature(Guid featureId, [FromBody]CarBrandRequest carBrandController)
    {
        return CustomResult("Success", await carBrandService.UpdateCarBrand(featureId, carBrandController));
    }

    [HttpDelete("DeleteFeature")]
    public async Task<IActionResult> DeleteFeature(Guid carBrandId)
    {
        return CustomResult("Success", await carBrandService.DeleteCarBrand(carBrandId));
    }
}