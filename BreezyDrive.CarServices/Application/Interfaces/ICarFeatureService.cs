using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Domain.Entities;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface ICarFeatureService
{
    Task<IEnumerable<CarFeatureResponse>> GetAllAsync();
    
    Task<CarFeatureResponse> GetByGuid(Guid guid);
    
    Task<CarFeatureResponse> CreateCarFeature(CarBrandRequest request);
    
    Task<CarFeatureResponse> UpdateCarFeature(Guid guid, CarBrandRequest request); 
    
    Task<bool> DeleteCarFeature(Guid guid);
}