using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface IRuleService
{
    Task<IEnumerable<RuleResponse>> GetAllAsync();
    
    Task<RuleResponse> GetByGuidAsync(Guid guid);
    
    Task<RuleResponse> CreateRule(RuleRequest request);
    
    Task<RuleResponse> UpdateRule(Guid guid, RuleRequest request); 
    
    Task<bool> DeleteRule(Guid guid);
    bool IsRuleExists(Guid ruleId);
}