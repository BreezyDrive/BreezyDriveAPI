using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;

namespace BreezyDrive.CarServices.Application.Services;

public class CarService : ICarService
{
    public Task<IEnumerable<CarResponse>> GetAllCarsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarResponse> GetByGuidAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<CarResponse> GetByModelName(string modelName)
    {
        throw new NotImplementedException();
    }

    public Task<CarResponse> Create(CarRequest carRequest)
    {
        throw new NotImplementedException();
    }

    public Task<CarResponse> Update(Guid guid, CarRequest carRequest)
    {
        throw new NotImplementedException();
    }

    public Task<CarResponse> DeleteCarByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }
}