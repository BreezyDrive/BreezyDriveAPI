using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface ICarRuleService
{
    Task<IEnumerable<CarRuleResponse>> GetAllAsync();
    
    Task<CarRuleResponse> GetByGuid(Guid guid);
    
    Task<CarRuleResponse> Create(CarRuleRequest request);
    
    Task<CarRuleResponse> Update(Guid guid, CarRuleRequest request);
    
    Task<CarRuleResponse> Delete(Guid guid);
}