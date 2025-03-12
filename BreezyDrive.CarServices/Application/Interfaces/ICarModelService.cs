using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.DTO.Responses;
using BreezyDrive.CarServices.Domain.Entities;

namespace BreezyDrive.CarServices.Application.Interfaces;

public interface ICarModelService
{
    Task<IEnumerable<CarModelResponse>> GetAllAsync();
    
    Task<CarModelResponse> GetByGuid(Guid guid);
    
    Task<CarModelResponse> GetByModelName(string modelName);
    
    Task<CarModelResponse> Create(CarModelRequest request);
    
    Task<CarModelResponse> Update(Guid guid, CarModelRequest request); 
    
    Task<CarModelResponse> Delete(Guid guid);
    

}