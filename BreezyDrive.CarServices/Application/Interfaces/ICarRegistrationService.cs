using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Domain.Entities;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface ICarRegistrationService
{
    Task<IEnumerable<CarRegistrationResponse>> GetAllAsync();
    Task<CarRegistrationResponse> GetByGuid(Guid guid);
    Task<CarRegistrationResponse> CreateCarRegistration(CarRegistrationRequest request);
    Task<CarRegistrationResponse> UpdateCarRegistration(Guid guid, CarRegistrationRequest request);
    Task<bool> Delete(Guid guid);
    
}