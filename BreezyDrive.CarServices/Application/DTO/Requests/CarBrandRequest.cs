using System.ComponentModel.DataAnnotations;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarBrandRequest : IMapFrom<CarBrands>
{
    [Required(ErrorMessage = "Vui lòng nhập hãng xe")]
    public string Name { get; set; }
}