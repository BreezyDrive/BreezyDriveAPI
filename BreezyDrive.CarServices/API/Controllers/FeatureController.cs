using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.CarServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeatureController(IFeatureService featureService) : BaseController
{
    [HttpGet("GetAllFeatures")]
    public async Task<IActionResult> GetAllFeatures()
    {
        return CustomResult("Success", await featureService.GetAllAsync());
    }

    [HttpGet("GetFeatureByGuid")]
    public async Task<IActionResult> GetFeatureByGuid(Guid featureGuid)
    {
        return CustomResult("Success", await featureService.GetByGuidAsync(featureGuid));
    }

    [HttpPost("CreateFeature")]
    public async Task<IActionResult> CreateFeature([FromBody]FeatureRequest featureRequest)
    {
        return CustomResult("Success", await featureService.CreateFeature(featureRequest));
    }

    [HttpPatch("UpdateFeature")]
    public async Task<IActionResult> UpdateFeature([FromBody]FeatureRequest featureRequest)
    {
        return CustomResult("Success", await featureService.UpdateFeature(featureRequest.Id, featureRequest));
    }

    [HttpDelete("DeleteFeature")]
    public async Task<IActionResult> DeleteFeature(Guid featureGuid)
    {
        return CustomResult("Success", await featureService.DeleteFeature(featureGuid));
    }
    
    
}