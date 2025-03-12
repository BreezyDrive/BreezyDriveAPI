using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Domain.Entities;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface ICarFeatureService
{
    Task<IEnumerable<CarFeatureResponse>> GetAllAsync();
    
    Task<CarFeatureResponse> GetByGuid(Guid guid);
    
    Task<CarFeatureResponse> Create(CarBrandRequest request);
    
    Task<CarFeatureResponse> Update(Guid guid, CarBrandRequest request); 
    
    Task<CarFeatureResponse> Delete(Guid guid);
}