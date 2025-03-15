using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Responses;

public class CarFeatureResponse : IMapFrom<CarFeatures>
{
    public Guid Id { get; set; }
    public Guid CarId { get; set; }
    
    public Guid FeatureId { get; set; }
    
    public virtual required Cars Car { get; set; }
    
    public virtual required Features Feature { get; set; }
}