using System.ComponentModel.DataAnnotations;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarRatingRequest : IMapFrom<CarRatings>
{
    public Guid UserId { get; set; }
    
    public Guid CarId {get; set;}
    
    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
    [Required(ErrorMessage = "Rating is required")]
    public float Star {get; set;}
    
    public string? Comment {get; set;}
}