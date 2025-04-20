using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Domain.Entities;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface ICarRatingService
{
    Task<IEnumerable<CarRatingsResponse>> GetAllAsync();
    
    Task<CarRatingsResponse> GetByGuid(Guid guid);
    
    Task<IEnumerable<CarRatingsResponse>> GetAllCarRatingsByCarGuidAsync(Guid carId);

    
    Task<CarRatingsResponse> CreateCarRating(CarRatingRequest request);
    
    Task<CarRatingsResponse> UpdateCarRating(Guid guid, CarRatingRequest request); 
    
    Task<bool> Delete(Guid guid);
    
}