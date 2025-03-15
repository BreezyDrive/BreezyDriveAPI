using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarBrandRequest : IMapFrom<CarBrands>
{
    public string Name { get; set; }
}