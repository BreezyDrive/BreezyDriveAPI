using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Domain.Entities;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface ICarService
{
    Task<IEnumerable<CarResponse>> GetAllCarsAsync();
    
    Task<CarResponse> GetByGuidAsync(Guid guid);
    
    Task<CarResponse> GetByModelName(string modelName);
    
    Task<CarResponse> Create(CarRequest carRequest);
    
    Task<CarResponse> Update(Guid guid, CarRequest carRequest);
    
    Task<bool> DeleteCarByGuid(Guid guid);

    Task<bool> CheckCarExist(Guid id);
}