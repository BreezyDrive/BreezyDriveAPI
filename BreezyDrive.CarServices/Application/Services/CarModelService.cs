using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.Common.Application.Mapper;

namespace BreezyDrive.CarServices.Application.Services;

public class CarModelService : ICarModelService
{
    public Task<IEnumerable<CarModelResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarModelResponse> GetByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<CarModelResponse> GetByModelName(string modelName)
    {
        throw new NotImplementedException();
    }

    public Task<CarModelResponse> Create(CarModelRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CarModelResponse> Update(Guid guid, CarModelRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CarModelResponse> Delete(Guid guid)
    {
        throw new NotImplementedException();
    }
}