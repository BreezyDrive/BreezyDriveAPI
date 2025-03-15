using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarRatingRequest : IMapFrom<CarRatings>
{
    public Guid UserId { get; set; }
    
    public Guid CarId {get; set;}
    
    public required float Star {get; set;}
    
    public string? Comment {get; set;}
}