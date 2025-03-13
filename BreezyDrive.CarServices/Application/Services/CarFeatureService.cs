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
        var carFeatures = new List<CarFeatureResponse>();
        
        if (carFeatures.IsNullOrEmpty())
        {
            throw new CustomExceptions.DataNotFoundException("Car Features");
        }

        return mapper.Map<IEnumerable<CarFeatureResponse>>(carFeatures);
    }

    public async Task<CarFeatureResponse> GetByGuid(Guid guid)
    {
        var carFeature = await unitOfWork.Repository<CarResponse>().GetByIdAsync(guid);
        if (carFeature == null)
        {
            throw new CustomExceptions.DataNotFoundException("Car Feature");
        }
        return mapper.Map<CarFeatureResponse>(carFeature);
    }

    public async Task<CarFeatureResponse> CreateCarFeature(CarBrandRequest request)
    {
        var carFeature = mapper.Map<CarFeatures>(request);
        
        await unitOfWork.Repository<CarFeatures>().InsertAsync(carFeature);
        
        await unitOfWork.SaveAsync();
        
        return mapper.Map<CarFeatureResponse>(carFeature);
        
    }

    public async Task<CarFeatureResponse> UpdateCarFeature(Guid guid, CarBrandRequest request)
    {
        var carFeature = await unitOfWork.Repository<CarFeatures>().GetByIdAsync(guid);
        if (carFeature == null)
        {
            throw new CustomExceptions.DataNotFoundException("Car Feature");
        }
        
        mapper.Map(request, carFeature);
        await unitOfWork.Repository<CarFeatures>().UpdateAsync(carFeature);
        await unitOfWork.SaveAsync();
        
        return mapper.Map<CarFeatureResponse>(carFeature);
    }

    public async Task<bool> DeleteCarFeature(Guid guid)
    {
        var carFeature = await unitOfWork.Repository<CarFeatures>().GetByIdAsync(guid);
        if (carFeature == null)
        {
            throw new CustomExceptions.DataNotFoundException("Car Feature");
        }
        await unitOfWork.Repository<CarFeatures>().DeleteAsync(guid);

        return true;
        
    }
}