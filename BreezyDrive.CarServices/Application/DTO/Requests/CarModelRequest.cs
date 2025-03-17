using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarModelRequest : IMapFrom<CarModels>
{
    public Guid BrandId { get; set; }
    
    public required string Name { get; set; }
    
    public required int ReleaseYear { get; set; }
}