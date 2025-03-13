using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Responses;

public class RuleResponse : IMapFrom<Rules>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

}