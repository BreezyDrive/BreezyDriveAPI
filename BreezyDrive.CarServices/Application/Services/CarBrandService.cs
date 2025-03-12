using AutoMapper;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.Common.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace BreezyDrive.CarServices.Application.Services;

public class CarBrandService (IUnitOfWork unitOfWork, IMapper mapper) : ICarBrandService
{
    public async Task<IEnumerable<CarBrandResponse>> GetAllAsync()
    {
        var carBrands = await unitOfWork.Repository<CarBrands>().GetAllAsync();

        if (carBrands.IsNullOrEmpty())
        {
            throw new CustomExceptions.DataNotFoundException("No carBrands found");
        }

        var carBrandsResponses = mapper.Map<IEnumerable<CarBrandResponse>>(carBrands);
        
        return carBrandsResponses;    
        
    }

    public async Task<CarBrandResponse> GetByGuid(Guid guid)
    {
        var carBrand = await unitOfWork.Repository<CarBrands>().GetByIdAsync(guid);
        
        if (carBrand == null)
        {
            throw new CustomExceptions.DataNotFoundException("No carBrand found");
        }
        var carBrandResponse = mapper.Map<CarBrandResponse>(carBrand);
        
        return carBrandResponse;
        
    }

    public async Task<CarBrandResponse> Create(CarBrandRequest request)
    {
        var carBrand = mapper.Map<CarBrands>(request);
        await unitOfWork.Repository<CarBrands>().InsertAsync(carBrand);
        
        await unitOfWork.SaveAsync();
        
        return mapper.Map<CarBrandResponse>(carBrand);
    }

    public Task<CarBrandResponse> Update(Guid guid, CarBrandRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<CarBrandResponse> Delete(Guid guid)
    {
        throw new NotImplementedException();
    }
}