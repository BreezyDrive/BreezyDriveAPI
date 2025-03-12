using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;

namespace BreezyDrive.CarServices.Application.Services;

public class CarRatingService : ICarRatingService
{
    public Task<IEnumerable<CarRatingsResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarRatingsResponse> GetByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<CarRatingsResponse> Create(CarModelRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CarRatingsResponse> Update(Guid guid, CarRatingRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CarRatingsResponse> Delete(Guid guid)
    {
        throw new NotImplementedException();
    }
}