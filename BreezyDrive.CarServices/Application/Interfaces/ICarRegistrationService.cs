using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Domain.Entities;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface ICarRegistrationService
{
    Task<IEnumerable<CarRegistrationResponse>> GetAllAsync();
    Task<CarRegistrationResponse> GetByGuid(Guid guid);
    Task<CarRegistrationResponse> Create(CarRegistrationRequest request);
    Task<CarRegistrationResponse> Update(Guid guid, CarRegistrationRequest request);
    Task<bool> Delete(Guid guid);
    
}