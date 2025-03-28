using System.Reflection;
using AutoMapper;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BreezyDrive.CarServices.Application.Services;

public class CarRegistrationService (IUnitOfWork unitOfWork, IMapper mapper) : ICarRegistrationService
{
    public async Task<IEnumerable<CarRegistrationResponse>> GetAllAsync()
    {
        var carRegistrations = await unitOfWork.Repository<CarRegistrations>().GetAllAsync();
        if (carRegistrations.IsNullOrEmpty())
        {
            throw new CustomExceptions.DataNotFoundException("Car Registrations not found");
        }
        return mapper.Map<IEnumerable<CarRegistrationResponse>>(carRegistrations);
    }

    public async Task<CarRegistrationResponse> GetByGuid(Guid guid)
    {
        return mapper.Map<CarRegistrationResponse>(await GetCarRegistrationsByIdAsync(guid));
    }

    public async Task<CarRegistrationResponse> CreateCarRegistration(CarRegistrationRequest request)
    {
        var carRegistration = mapper.Map<CarRegistrations>(request);
        await unitOfWork.Repository<CarRegistrations>().InsertAsync(carRegistration);
        await unitOfWork.SaveAsync();
        return mapper.Map<CarRegistrationResponse>(carRegistration);
    }

    public async Task<CarRegistrationResponse> UpdateCarRegistration(Guid guid, CarRegistrationRequest request)
    {
        var carRegistation = await this.GetCarRegistrationsByIdAsync(guid);
        mapper.Map(request, carRegistation);
        await unitOfWork.Repository<CarRegistrations>().UpdateAsync(carRegistation);
        await unitOfWork.SaveAsync();
        return mapper.Map<CarRegistrationResponse>(carRegistation);
    }

    public async Task<bool> Delete(Guid guid)
    {
        var carRegistration = GetCarRegistrationsByIdAsync(guid);
        await unitOfWork.Repository<CarRegistrations>().DeleteAsync(carRegistration);
        return true;
    }

    private async Task<CarRegistrations> GetCarRegistrationsByIdAsync(Guid id)
    {
        var carRegistration = await unitOfWork.Repository<CarRegistrations>().GetByIdAsync(id);
        if (carRegistration == null)
        {
            throw new CustomExceptions.DataNotFoundException("Car Registration Not Found");
        }
        return carRegistration;
    }
}