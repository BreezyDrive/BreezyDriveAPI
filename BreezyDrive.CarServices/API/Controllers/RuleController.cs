using System.Data;
using System.Net;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CarServices.Application.Services;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.CarServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RuleController(IRuleService ruleService) : BaseController
{
    
    [HttpGet("GetAllRules")]
    public async Task<IActionResult> GetAllRules()
    {

        var rules = await ruleService.GetAllAsync();
        
        return CustomResult("Success", rules);
    }

    [HttpGet("GetRuleByGuid{id}")]
    public async Task<IActionResult> GetRuleByGuid(Guid id)
    {
        var rule = await ruleService.GetByGuidAsync(id);
        return CustomResult("Success", rule);
    }


    [HttpPost("CreateRule")]
    public async Task<IActionResult> CreateRule([FromBody] RuleRequest ruleRequest)
    {
        var rule = await ruleService.CreateRule(ruleRequest);
        return CustomResult("Success", rule);
        
    }

    [HttpPatch("UpdateRule")]
    public async Task<IActionResult> UpdateRule([FromBody] RuleRequest ruleRequest)
    {
        var rule = await ruleService.UpdateRule(ruleRequest.Id, ruleRequest);
        return CustomResult("Success", rule);
    }

    [HttpDelete("DeleteRule/{id}")]
    public async Task<IActionResult> DeleteRule(Guid id)
    {
        var isDeleted = await ruleService.DeleteRule(id);
        return CustomResult("Success", isDeleted);
    }
    
    
    
    
}