using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.CarServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarRuleController (ICarRuleService carRuleService) : BaseController
{
    [HttpGet("GetAllCarRules")]
    public async Task<IActionResult> GetAllCarRules()
    {
        return CustomResult("Success", await carRuleService.GetAllAsync());
    }

    [HttpGet("GetCarRuleById/{id}")]
    public async Task<IActionResult> GetCarRuleById(Guid id)
    {
        return CustomResult("Success", await carRuleService.GetByGuid(id));
    }

    [HttpPost("AddCarRule")]
    public async Task<IActionResult> AddCarRule(CarRuleRequest carRuleRequest)
    {
        return CustomResult("Success", await carRuleService.CreateCarRule(carRuleRequest));
    }

    [HttpPatch("UpdateCarRule/{carRuleId}")]
    public async Task<IActionResult> UpdateCarRule(Guid carRuleId ,CarRuleRequest carRuleRequest)
    {
        return CustomResult( "Success", await carRuleService.UpdateCarRule(carRuleId, carRuleRequest));
      
    }

    [HttpDelete("DeleteCarRule/{carRuleId}")]
    public async Task<IActionResult> DeleteCarRule(Guid carRuleId)
    {
        return CustomResult("Success", await carRuleService.Delete(carRuleId));
    }
}