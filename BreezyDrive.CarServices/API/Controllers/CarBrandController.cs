using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.CarServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarBrandController(ICarBrandService carBrandService) : BaseController
{
    [HttpGet("GetAllCarBrands")]
    public async Task<IActionResult> GetAllCarBrands()
    {
        return CustomResult("Success", await carBrandService.GetAllAsync());
    }

    [HttpGet("GetCarBrandById/{carBrandId}")]
    public async Task<IActionResult> GetCarBrandById(Guid carBrandId)
    {
        return CustomResult("Success", await carBrandService.GetByGuid(carBrandId));
    }

    [HttpPost("CreateCarBrand")]
    public async Task<IActionResult> CreateCarBrand([FromBody]CarBrandRequest carBrandController)
    {
        return CustomResult("Success", await carBrandService.CreateCarBrand(carBrandController));
    }

    [HttpPatch("UpdateCarBrand/{carBrandId}")]
    public async Task<IActionResult> UpdateCarBrand(Guid carBrandId, [FromBody]CarBrandRequest carBrandController)
    {
        return CustomResult("Success", await carBrandService.UpdateCarBrand(carBrandId, carBrandController));
    }

    [HttpDelete("DeleteCarBrand/{carBrandId}")]
    public async Task<IActionResult> DeleteFeature(Guid carBrandId)
    {
        return CustomResult("Success", await carBrandService.DeleteCarBrand(carBrandId));
    }
}