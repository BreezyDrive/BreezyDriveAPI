using System.ComponentModel.DataAnnotations;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class FeatureRequest : IMapFrom<Features>
{
    [Required(ErrorMessage = "Feature name is required")]
    public string Name { get; set; }
}