using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface ICarBrandService
{
    Task<IEnumerable<CarBrandResponse>> GetAllAsync();
    
    Task<CarBrandResponse> GetByGuid(Guid guid);
    
    Task<CarBrandResponse> CreateCarBrand(CarBrandRequest request);
    
    Task<CarBrandResponse> UpdateCarBrand(Guid guid, CarBrandRequest request); 
    
    Task<bool> DeleteCarBrand(Guid guid);
    bool IsBrandExists(Guid brandId);
}