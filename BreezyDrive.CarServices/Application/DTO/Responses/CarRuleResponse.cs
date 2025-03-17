using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Responses;

public class CarRuleResponse : IMapFrom<CarRules>
{
    public Guid Id { get; set; }
    
    public Guid CarId { get; set; }

    public Guid RuleId { get; set; }
    
    public virtual required Cars Car { get; set; }
    
    public virtual required Rules Rule { get; set; }
    
}