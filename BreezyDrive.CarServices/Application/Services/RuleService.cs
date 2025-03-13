using AutoMapper;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace BreezyDrive.CarServices.Application.Services;

public class RuleService(IUnitOfWork unitOfWork, IMapper mapper) : IRuleService
{
    
    public async Task<IEnumerable<RuleResponse>> GetAllAsync()
    {
         var rules = await unitOfWork.Repository<Rules>().GetAllAsync();

         if (rules.IsNullOrEmpty())
         {
           throw new CustomExceptions.DataNotFoundException("No rules found");
         }

         var ruleResponses = mapper.Map<IEnumerable<RuleResponse>>(rules);
        
        return ruleResponses;
    }

    public async Task<RuleResponse> GetByGuidAsync(Guid guid)
    {
        var rule = await unitOfWork.Repository<Rules>().GetByIdAsync(guid);
        if (rule == null)
        {
            throw new CustomExceptions.DataNotFoundException("No rule found");
        }
        var ruleResponse = mapper.Map<RuleResponse>(rule);
        
        return ruleResponse;
    }

    public async Task<RuleResponse> CreateRule(RuleRequest request)
    {
        var rule = mapper.Map<Rules>(request);
        await unitOfWork.Repository<Rules>().InsertAsync(rule);
        
        await unitOfWork.SaveAsync();
        
        return mapper.Map<RuleResponse>(rule);

    }

    
    public async Task<RuleResponse> UpdateRule(Guid guid, RuleRequest request)
    {
        var rule = await unitOfWork.Repository<Rules>().GetByIdAsync(guid);

        if (rule == null)
        {
            throw new CustomExceptions.DataNotFoundException("No rule found");
        }
        
        mapper.Map(request, rule);
        await unitOfWork.Repository<Rules>().UpdateAsync(rule);
        await unitOfWork.SaveAsync();
        
        return mapper.Map<RuleResponse>(rule);
    }

    public async Task<bool> DeleteRule(Guid guid)
    {
        var rule = await unitOfWork.Repository<Rules>().GetByIdAsync(guid);
        if (rule == null)
        {
            throw new CustomExceptions.DataNotFoundException("No rule found");
        }
        await unitOfWork.Repository<Rules>().DeleteAsync(guid);

        return true;
    }
}