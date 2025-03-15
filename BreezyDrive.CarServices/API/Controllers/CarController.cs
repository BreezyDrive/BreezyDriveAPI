using System.Security.AccessControl;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.CarServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController (ICarService carService) : BaseController
{
    [HttpGet("GetAllCars")]
    public async Task<IActionResult> GetAllCars()
    {
        return CustomResult("Success", await carService.GetAllCarsAsync());
    }

    [HttpGet("GetCarByGuid/{id}")]
    public async Task<IActionResult> GetCarByGuid(Guid id)
    {
        return CustomResult("Success", await carService.GetByGuidAsync(id));
    }
    
    [HttpPost("AddCar")] 
    public async Task<IActionResult> CreateCar ([FromBody] CarRequest carRequest) {
        return CustomResult("Success", await carService.Create(carRequest));
        
    }

    [HttpPatch("UpdateCar/{carId}")]
    public async Task<IActionResult> UpdateCar(Guid carId, [FromBody] CarRequest carRequest)
    {
        return CustomResult("Success", await carService.Update(carId, carRequest));
    }

    [HttpDelete("DeleteCar")]
    public async Task<IActionResult> DeleteCar(Guid id)
    {
        return CustomResult("Success", await carService.DeleteCarByGuid(id));
    }



}