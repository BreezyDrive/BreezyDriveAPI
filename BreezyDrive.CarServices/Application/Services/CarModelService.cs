using AutoMapper;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;

namespace BreezyDrive.CarServices.Application.Services;

public class CarModelService(IUnitOfWork unitOfWork, IMapper mapper) : ICarModelService
{
    public async Task<IEnumerable<CarModelResponse>> GetAllAsync()
    {
        var carModels = await unitOfWork.Repository<CarModels>().GetAllAsync();

        if (carModels == null)
        {
            throw new CustomExceptions.DataNotFoundException("Car Models not found");
        }
        
        return mapper.Map<IEnumerable<CarModelResponse>>(carModels);
    }

    public async Task<CarModelResponse> GetByGuid(Guid guid)
    {
        
        var carModel = await unitOfWork.Repository<CarModels>().GetByIdAsync(guid);
        if (carModel == null)
        {
            throw new CustomExceptions.DataNotFoundException("Car Model not found");
        }
        
        return mapper.Map<CarModelResponse>(carModel);
    }

    public async Task<CarModelResponse> CreateCarModel(CarModelRequest request)
    {
        var carModel = mapper.Map<CarModels>(request);
        await unitOfWork.Repository<CarModels>().InsertAsync(carModel);

        await unitOfWork.SaveAsync();
        return mapper.Map<CarModelResponse>(carModel);

    }

    public async Task<CarModelResponse> UpdateCarModel(Guid guid, CarModelRequest request)
    {
        var carModel = await unitOfWork.Repository<CarModels>().GetByIdAsync(guid);
        if (carModel == null)
        {
            throw new CustomExceptions.DataNotFoundException("Car Model not found");
        }
        
        mapper.Map(request, carModel);
        
        await unitOfWork.Repository<CarModels>().UpdateAsync(carModel);
        
        await unitOfWork.SaveAsync();
        return mapper.Map<CarModelResponse>(carModel);
    }

    public async Task<bool> DeleteCarModel(Guid guid)
    {
        var carModel = await unitOfWork.Repository<CarModels>().GetByIdAsync(guid);
        if (carModel == null)
        {
            throw new CustomExceptions.DataNotFoundException("Car Model not found");
        }
        
        await unitOfWork.Repository<CarModels>().DeleteAsync(guid);
        
        return true;
    }
}