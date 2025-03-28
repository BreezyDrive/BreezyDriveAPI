using System.ComponentModel.DataAnnotations;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarRatingRequest : IMapFrom<CarRatings>
{
    public Guid UserId { get; set; }
    
    public Guid CarId {get; set;}
    
    [Range(0, 5, ErrorMessage = "Đánh giá chỉ từ 0 đến 5 ")]
    [Required(ErrorMessage = "Vui lòng nhập số sao")]
    public float Star {get; set;}
    
    public string? Comment {get; set;}
}