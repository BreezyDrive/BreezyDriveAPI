using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface ICarRuleService
{
    Task<IEnumerable<CarRuleResponse>> GetAllAsync();
    
    Task<CarRuleResponse> GetByGuid(Guid guid);
    
    Task<CarRuleResponse> CreateCarRule(CarRuleRequest request);
    
    Task<CarRuleResponse> UpdateCarRule(Guid guid, CarRuleRequest request);
    
    Task<bool> Delete(Guid guid);
}