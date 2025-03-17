using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarFeatureRequest : IMapFrom<CarFeatures>
{
    public Guid CarId { get; set; }
    
    public Guid FeatureId { get; set; }
}