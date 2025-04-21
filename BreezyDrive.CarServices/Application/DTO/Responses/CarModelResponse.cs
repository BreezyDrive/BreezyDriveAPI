using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Responses;

public class CarModelResponse : IMapFrom<CarModels>
{
    public Guid Id { get; set; }
    // public Guid BrandId { get; set; }
    public CarBrandResponse CarBrand { get; set; }
    
    public required string Name { get; set; }
    
    public required int ReleaseYear { get; set; }
    
}