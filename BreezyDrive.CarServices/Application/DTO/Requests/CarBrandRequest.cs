using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarBrandRequest : IMapFrom<CarBrands>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
}