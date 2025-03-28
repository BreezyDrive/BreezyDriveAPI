using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.CarServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarRegistrationController (ICarRegistrationService carRegistrationService) : BaseController
{
   [HttpGet("GetAllCarRegistrations")]
   public async Task<IActionResult> GetAllCarRegistrationsAsync()
   {
      return CustomResult("Success", await carRegistrationService.GetAllAsync());
   }

   [HttpGet("GetCarRegistrationById/{id}")]
   public async Task<IActionResult> GetCarRegistrationByIdAsync(Guid id)
   {
      return CustomResult("Success", await carRegistrationService.GetByGuid(id));
   }

   [HttpPost("AddCarRegistration")]
   public async Task<IActionResult> AddCarRegistration(CarRegistrationRequest carRegistrationRequest)
   {
      return CustomResult("Success", await carRegistrationService.CreateCarRegistration(carRegistrationRequest));
   }

   [HttpPatch("UpdateCarRegistration/{carRegistrationId}")]
   public async Task<IActionResult> UpdateCarRegistration(Guid carRegistrationId ,CarRegistrationRequest carRegistrationRequest)
   {
      return CustomResult( "Success", await carRegistrationService.UpdateCarRegistration(carRegistrationId, carRegistrationRequest));
      
   }

   [HttpDelete("DeleteCarRegistration/{carRegistrationId}")]
   public async Task<IActionResult> DeleteCarRegistration(Guid carRegistrationId)
   {
      return CustomResult("Success", await carRegistrationService.Delete(carRegistrationId));
   }
   
   
   
}