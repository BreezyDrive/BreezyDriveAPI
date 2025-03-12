using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;

namespace BreezyDrive.CarServices.Application.Services;

public class CarFeatureService : ICarFeatureService
{
    public Task<IEnumerable<CarFeatureResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarFeatureResponse> GetByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<CarFeatureResponse> Create(CarBrandRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CarFeatureResponse> Update(Guid guid, CarBrandRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CarFeatureResponse> Delete(Guid guid)
    {
        throw new NotImplementedException();
    }
}