using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Responses;

public class CarBrandResponse : IMapFrom<CarBrands>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

}