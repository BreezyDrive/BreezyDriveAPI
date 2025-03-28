using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarRuleRequest : IMapFrom<CarRules>
{
    public Guid CarId { get; set; }

    public Guid RuleId { get; set; }
    
}