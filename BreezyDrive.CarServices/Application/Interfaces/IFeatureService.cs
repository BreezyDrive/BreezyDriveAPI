using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface IFeatureService
{
    Task<IEnumerable<FeatureResponse>> GetAllAsync();
    
    Task<FeatureResponse> GetByGuidAsync(Guid guid);
    
    Task<FeatureResponse> Create(FeatureRequest request);
    
    Task<FeatureResponse> Update(Guid guid, FeatureRequest request); 
    
    Task<bool> Delete(Guid guid);
}