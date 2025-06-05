using System.Net;
using System.Security.AccessControl;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using CoreApiResponse;
using FluentValidation;
using FluentValidation.AspNetCore;
using Library.EventContracts.Events;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.CarServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController(ICarService carService, IRequestClient<CheckUserExistRequestEvent> client) : BaseController
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
        
       return CustomResult("Success", await carService.CreateCar(carRequest));
       
    }

    [HttpPatch("UpdateCar/{carId}")]
    public async Task<IActionResult> UpdateCar(Guid carId, [FromBody] CarRequest carRequest)
    {
        return CustomResult("Success", await carService.UpdateCar(carId, carRequest));
    }

    [HttpDelete("DeleteCar")]
    public async Task<IActionResult> DeleteCar(Guid id)
    {
        return CustomResult("Success", await carService.DeleteCarByGuid(id));
    }
    
    //test rabbitmq
    [HttpGet("CheckIfUserExist/{id}")]
    public async Task<IActionResult> CheckIfUserExist(Guid id)
    {
        var response = await client.GetResponse<CheckUserExistResponse>(
            new CheckUserExistRequestEvent { UserId = id });
        
        return CustomResult("Success", response);
    }



}