using System.ComponentModel.DataAnnotations;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class RuleRequest : IMapFrom<Rules>
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}