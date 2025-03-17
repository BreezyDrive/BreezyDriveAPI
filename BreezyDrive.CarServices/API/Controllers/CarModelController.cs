using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.CarServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarModelController (ICarModelService carModelService) : BaseController
{
    [HttpGet("GetAllCarModels")]
    public async Task<IActionResult> GetAllCarModels()
    {
        return CustomResult("Success", await carModelService.GetAllAsync());
    }

    [HttpGet("GetCarModelByGuid/{id}")]
    public async Task<IActionResult> GetCarModelByGuid(Guid id)
    {
        return CustomResult("Success", await carModelService.GetByGuid(id));
    }

    [HttpPost("AddCarModel")]
    public async Task<IActionResult> CreateCar([FromBody] CarModelRequest carModelRequest)
    {
        return CustomResult("Success", await carModelService.CreateCarModel(carModelRequest));
    }

    [HttpPatch("UpdateCarModel/{carModelId}")]
    public async Task<IActionResult> UpdateCarModel(Guid carModelId, [FromBody] CarModelRequest carModelRequest)
    {
        return CustomResult("Success", await carModelService.UpdateCarModel(carModelId, carModelRequest));
    }

    [HttpDelete("DeleteCarModel/{id}")]
    public async Task<IActionResult> DeleteCarModel(Guid id)
    {
        return CustomResult("Success", await carModelService.DeleteCarModel(id));
    }
    
    
}