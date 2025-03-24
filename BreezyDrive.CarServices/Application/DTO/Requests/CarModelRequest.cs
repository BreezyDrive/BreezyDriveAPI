using System.ComponentModel.DataAnnotations;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarModelRequest : IMapFrom<CarModels>
{
    public Guid BrandId { get; set; }
    
    [Required(ErrorMessage = "Model Name is required")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Release Year is required")]
    public int ReleaseYear { get; set; }
}