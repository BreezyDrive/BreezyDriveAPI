using System.ComponentModel.DataAnnotations;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarModelRequest : IMapFrom<CarModels>
{
    public Guid BrandId { get; set; }
    
    [Required(ErrorMessage = "Vui lòng nhập Model xe")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Vui lòng nhập năm phát hành của xe")]
    public int ReleaseYear { get; set; }
}