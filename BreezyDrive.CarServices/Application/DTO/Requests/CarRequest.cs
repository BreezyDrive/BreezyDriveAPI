using System.ComponentModel.DataAnnotations;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CarServices.Domain.Enums;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarRequest : IMapFrom<Cars>
{
    
    public Guid UserId { get; set; }
    
    public Guid CarModelId { get; set; }

    [Required(ErrorMessage = "Vui lòng cung cấp hình ảnh")]
    public string CarAvatar { get; set; }

    public string? FrontImage { get; set; }

    public string? BackImage { get; set; }

    public string? LeftImage { get; set; }

    public string? RightImage { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập xe số sàn hay số tự động")]
    public TransmissionTypeEnum TransmissionType { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập loại nhiên liệu")]
    public string FuelType { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mức tiêu thụ nhiên liệu.")]
    [Range(1, int.MaxValue, ErrorMessage = "Mức tiêu thụ nhiên liệu phải lớn hơn 0.")]
    public int FuelConsumption { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số ghế.")]
    public int Seat { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập vị trí.")]
    public string Location { get; set; }

    public string? Description { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập ngày đăng ký.")]
    public DateOnly DayOfRegistration { get; set; }

    [Required(ErrorMessage = "Vui lòng xác định có giao xe tận nơi hay không")]
    public bool IsDropOf { get; set; }
    
    public int? FeePerKm { get; set; }
    
    public int? AvailableZone { get; set; }
    
    [Required(ErrorMessage = "Vui lòng nhập giá thuê theo ngày.")]
    public double PricePerDay { get; set; }
    
}