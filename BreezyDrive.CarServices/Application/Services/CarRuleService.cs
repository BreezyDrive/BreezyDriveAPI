using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;

namespace BreezyDrive.CarServices.Application.Services;

public class CarRuleService : ICarRuleService
{
    public Task<IEnumerable<CarRuleResponse>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CarRuleResponse> GetByGuid(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<CarRuleResponse> Create(CarRuleRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CarRuleResponse> Update(Guid guid, CarRuleRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CarRuleResponse> Delete(Guid guid)
    {
        throw new NotImplementedException();
    }
}