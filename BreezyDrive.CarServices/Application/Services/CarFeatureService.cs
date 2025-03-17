using AutoMapper;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace BreezyDrive.CarServices.Application.Services;

public class CarFeatureService (IUnitOfWork unitOfWork, IMapper mapper) : ICarFeatureService
{
    public async Task<IEnumerable<CarFeatureResponse>> GetAllAsync()
    {
        var carFeatures = await unitOfWork.Repository<CarFeatures>().GetAllAsync();
        
        if (carFeatures.IsNullOrEmpty())
        {
            throw new CustomExceptions.DataNotFoundException("Car Features");
        }

        return mapper.Map<IEnumerable<CarFeatureResponse>>(carFeatures);
    }

    public async Task<CarFeatureResponse> GetByGuid(Guid guid)
    {
        var carFeature = await GetCarFeatureById(guid);

        return mapper.Map<CarFeatureResponse>(carFeature);
    }

    public async Task<CarFeatureResponse> CreateCarFeature(CarFeatureRequest request)
    {
        var carFeature = mapper.Map<CarFeatures>(request);
        
        await unitOfWork.Repository<CarFeatures>().InsertAsync(carFeature);
        
        await unitOfWork.SaveAsync();
        
        return mapper.Map<CarFeatureResponse>(carFeature);
        
    }

    public async Task<CarFeatureResponse> UpdateCarFeature(Guid guid, CarFeatureRequest request)
    {
        var carFeature = await GetCarFeatureById(guid);
        
        mapper.Map(request, carFeature);
        await unitOfWork.Repository<CarFeatures>().UpdateAsync(carFeature);
        await unitOfWork.SaveAsync();
        
        return mapper.Map<CarFeatureResponse>(carFeature);
    }

    public async Task<bool> DeleteCarFeature(Guid guid)
    {
        var carFeature = await GetCarFeatureById(guid);
        await unitOfWork.Repository<CarFeatures>().DeleteAsync(guid);

        return true;
        
    }
    
    private async Task<CarFeatures> GetCarFeatureById(Guid guid)
    {
        var carFeature = await unitOfWork.Repository<CarFeatures>().GetByIdAsync(guid);
        if (carFeature == null)
        {
            throw new CustomExceptions.DataNotFoundException("Car Ratings not found");
        }
        return carFeature;
    }
}