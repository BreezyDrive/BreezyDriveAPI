using AutoMapper;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BreezyDrive.CarServices.Application.Services;

public class CarService (IUnitOfWork unitOfWork, IMapper mapper) : ICarService
{
    public async Task<IEnumerable<CarResponse>> GetAllCarsAsync()
    {
        var cars = await unitOfWork.Repository<Cars>().GetAllAsync();
        if (cars.IsNullOrEmpty())
        {
            throw new CustomExceptions.DataNotFoundException("No cars found");
        }
        return mapper.Map<IEnumerable<CarResponse>>(cars);
    }

    public async Task<CarResponse> GetByGuidAsync(Guid guid)
    {
        return mapper.Map<CarResponse>(await this.GetCarByIdAsync(guid));
    }

    public Task<CarResponse> GetByModelName(string modelName)
    {
        throw new NotImplementedException();
    }

    public async Task<CarResponse> CreateCar(CarRequest carRequest)
    {
        var car = mapper.Map<Cars>(carRequest);
        await unitOfWork.Repository<Cars>().InsertAsync(car);
        await unitOfWork.SaveAsync();
        return mapper.Map<CarResponse>(car);
    }

    public async Task<CarResponse> UpdateCar(Guid guid, CarRequest carRequest)
    {
        var car = await this.GetCarByIdAsync(guid);
        mapper.Map(carRequest, car);
        await unitOfWork.Repository<Cars>().UpdateAsync(car);
        await unitOfWork.SaveAsync();
        return mapper.Map<CarResponse>(car);
    }

    public async Task<bool> DeleteCarByGuid(Guid guid)
    {
        await this.GetCarByIdAsync(guid);
        await unitOfWork.Repository<Cars>().DeleteAsync(guid);
        await unitOfWork.SaveAsync();
        return true;
    }

    public bool IsCarExists(Guid carId)
    {
        return unitOfWork.Repository<Cars>().Exists(u => u.Id == carId);
    }

    private async Task<Cars> GetCarByIdAsync(Guid carId)
    {
        var car = await unitOfWork.Repository<Cars>().GetByIdAsync(carId);
        if (car == null)
        {
            throw new CustomExceptions.DataNotFoundException("Car not found");
        }
        return car;
    }
}