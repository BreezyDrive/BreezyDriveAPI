using AutoMapper;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace BreezyDrive.CarServices.Application.Services;

public class FeatureService (IUnitOfWork unitOfWork, IMapper mapper) : IFeatureService
{
    
    public async Task<IEnumerable<FeatureResponse>> GetAllAsync()
    {
        var features = await unitOfWork.Repository<Features>().GetAllAsync();

        if (features.IsNullOrEmpty())
        {
            throw new CustomExceptions.DataNotFoundException("No rules found");
        }

        var featureResponses = mapper.Map<IEnumerable<FeatureResponse>>(features);
        
        return featureResponses;
    }
    

    public async Task<FeatureResponse> GetByGuidAsync(Guid guid)
    {
        var feature = await unitOfWork.Repository<Rules>().GetByIdAsync(guid);
        if (feature == null)
        {
            throw new CustomExceptions.DataNotFoundException("No rule found");
        }
        var featureResponse = mapper.Map<FeatureResponse>(feature);
        
        return featureResponse;
    }

    public async Task<FeatureResponse> CreateFeature(FeatureRequest request)
    {
        var feature = mapper.Map<Features>(request);
        await unitOfWork.Repository<Features>().InsertAsync(feature);
        
        await unitOfWork.SaveAsync();
        
        return mapper.Map<FeatureResponse>(feature);

    }

    
    public async Task<FeatureResponse> UpdateFeature(Guid guid, FeatureRequest request)
    {
        var feature = await unitOfWork.Repository<Features>().GetByIdAsync(guid);

        if (feature == null)
        {
            throw new CustomExceptions.DataNotFoundException("No feature found");
        }
        
        mapper.Map(request, feature);
        await unitOfWork.Repository<Features>().UpdateAsync(feature);
        await unitOfWork.SaveAsync();
        
        return mapper.Map<FeatureResponse>(feature);
    }

    public async Task<bool> DeleteFeature(Guid guid)
    {
        var feature = await unitOfWork.Repository<Features>().GetByIdAsync(guid);
        if (feature == null)
        {
            throw new CustomExceptions.DataNotFoundException("No rule found");
        }
        await unitOfWork.Repository<Features>().DeleteAsync(guid);

        return true;
    }
}