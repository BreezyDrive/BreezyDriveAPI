using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface IRuleService
{
    Task<IEnumerable<RuleResponse>> GetAllAsync();
    
    Task<RuleResponse> GetByGuidAsync(Guid guid);
    
    Task<RuleResponse> Create(RuleRequest request);
    
    Task<RuleResponse> Update(Guid guid, RuleRequest request); 
    
    Task<bool> Delete(Guid guid);
}