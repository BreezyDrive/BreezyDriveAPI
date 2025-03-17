using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Responses;

public class FeatureResponse : IMapFrom<Features>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

}