using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface IFeatureService
{
    Task<IEnumerable<FeatureResponse>> GetAllAsync();
    
    Task<FeatureResponse> GetByGuidAsync(Guid guid);
    
    Task<FeatureResponse> CreateFeature(FeatureRequest request);
    
    Task<FeatureResponse> UpdateFeature(Guid guid, FeatureRequest request); 
    
    Task<bool> DeleteFeature(Guid guid);
    bool IsFeatureExists(Guid featureId);
}