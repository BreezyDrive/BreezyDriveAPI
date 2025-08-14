using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Domain.Entities;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface ICarService
{
    Task<IEnumerable<CarResponse>> GetAllCarsAsync();
    
    Task<IEnumerable<CarResponse>> GetAllCarsFilterByDateAsync(DateOnly startDate, DateOnly endDate);
    
    
    Task<CarResponse> GetByGuidAsync(Guid guid);
    
    Task<CarResponse> GetByModelName(string modelName);
    
    Task<CarResponse> CreateCar(CarRequest carRequest);
    
    Task<CarResponse> UpdateCar(Guid guid, CarRequest carRequest);
    
    Task<bool> DeleteCarByGuid(Guid guid);
    
    bool IsCarExists(Guid carId);

    Task<bool> CheckCarExist(Guid id);
}