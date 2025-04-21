using System.ComponentModel.DataAnnotations;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class RuleRequest : IMapFrom<Rules>
{
    [Required(ErrorMessage = "Vui lòng thêm tên luật")]
    public string Name { get; set; }
}