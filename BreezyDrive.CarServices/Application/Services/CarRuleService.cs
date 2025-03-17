using AutoMapper;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BreezyDrive.CarServices.Application.Services;

public class CarRuleService (IUnitOfWork unitOfWork, IMapper mapper) : ICarRuleService
{
    public async Task<IEnumerable<CarRuleResponse>> GetAllAsync()
    {
        var carRules = await unitOfWork.Repository<CarRules>().GetAllAsync();
        if (carRules.IsNullOrEmpty())
        {
            throw new CustomExceptions.DataNotFoundException("Car rules not found");
        }
        return mapper.Map<IEnumerable<CarRuleResponse>>(carRules);
    }

    public async Task<CarRuleResponse> GetByGuid(Guid guid)
    {
        return mapper.Map<CarRuleResponse>(await this.GetCarRuleById(guid));
    }

    public async Task<CarRuleResponse> Create(CarRuleRequest request)
    {
        var carRule = mapper.Map<CarRules>(request);
        await unitOfWork.Repository<CarRules>().InsertAsync(carRule);
        await unitOfWork.SaveAsync();
        return mapper.Map<CarRuleResponse>(carRule);
        
    }

    public async Task<CarRuleResponse> Update(Guid guid, CarRuleRequest request)
    {
        var carRule = await this.GetCarRuleById(guid);
        mapper.Map(request, carRule);
        
        await unitOfWork.Repository<CarRules>().UpdateAsync(carRule);
        await unitOfWork.SaveAsync();
        
        return mapper.Map<CarRuleResponse>(carRule);
    }

    public async Task<bool> Delete(Guid guid)
    {
        var carRule = await this.GetCarRuleById(guid);
        await unitOfWork.Repository<CarRules>().DeleteAsync(carRule);
        await unitOfWork.SaveAsync();
        return true;
    }

    private async Task<CarRules> GetCarRuleById(Guid guid)
    {
        var carRule = await unitOfWork.Repository<CarRules>().GetByIdAsync(guid);
        if (carRule == null)
        {
            throw new CustomExceptions.DataNotFoundException("Car rule not found");
        }

        return carRule;
    }
}