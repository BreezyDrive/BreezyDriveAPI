using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;

namespace BreezyDrive.CarServices.Application.Services;

public class CarRegistrationService : ICarRegistrationService
{
    public Task<IEnumerable<CarRegistrationResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarRegistrationResponse> GetByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<CarRegistrationResponse> Create(CarRegistrationRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CarRegistrationResponse> Update(Guid guid, CarRegistrationRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CarRegistrationResponse> Delete(Guid guid)
    {
        throw new NotImplementedException();
    }
}