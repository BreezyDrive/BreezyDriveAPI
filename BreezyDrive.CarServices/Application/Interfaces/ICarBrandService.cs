using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface ICarBrandService
{
    Task<IEnumerable<CarBrandResponse>> GetAllAsync();
    
    Task<CarBrandResponse> GetByGuid(Guid guid);
    
    Task<CarBrandResponse> Create(CarBrandRequest request);
    
    Task<CarBrandResponse> Update(Guid guid, CarBrandRequest request); 
    
    Task<CarBrandResponse> Delete(Guid guid);
}