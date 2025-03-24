using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.CarServices.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarRatingController(ICarRatingService carRatingService) : BaseController
{
    [HttpGet("GetAllCarRatings")]
    public async Task<IActionResult> GetAllCarRatings()
    {
        return CustomResult("Success", await carRatingService.GetAllAsync());
    }

    [HttpGet("GetRatingByCarId/{carRatingId}")]
    public async Task<IActionResult> GetCarRatingByCarId(Guid carRatingId)
    {
        return CustomResult("Success", await carRatingService.GetByGuid(carRatingId));
    }

    [HttpPost("AddCarRating")]
    public async Task<IActionResult> AddCarRating([FromBody] CarRatingRequest carRatingRequest)
    {
        return CustomResult("Success", await carRatingService.CreateCarRating(carRatingRequest));    
    }

    [HttpPatch("UpdateCarRating/{carRatingId}")]
    public async Task<IActionResult> UpdateCarRating(Guid carRatingId ,[FromBody] CarRatingRequest carRatingRequest)
    {
        return CustomResult("Success", await carRatingService.UpdateCarRating(carRatingId, carRatingRequest));
    }

    [HttpDelete("DeleteCarRating/{carRatingId}")]
    public async Task<IActionResult> DeleteCarRating(Guid carRatingId)
    {
        return CustomResult("Success", await carRatingService.Delete(carRatingId));
    }
    
    
    
}