using System.ComponentModel.DataAnnotations;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarRegistrationRequest : IMapFrom<CarRegistrations>
{
    [Required(ErrorMessage = "Vui lòng nhập Mã xe.")]
    public Guid CarId { get; set; }

    [Required(ErrorMessage = "Vui lòng cung cấp Hình ảnh.")]
    public string Image { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số khung")]
    public string VehicleIdentificationNumber { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số máy")]
    public string EngineIdentificationNumber { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Dung tích.")]
    public string Capacity { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Màu sắc.")]
    public string Color { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Công suất xe.")]
    public string HorsePower { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Năm sản xuất.")]
    public string YearOfManufacture { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Trọng lượng không tải.")]
    public string EmptyWeight { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Chiều dài.")]
    public string Length { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Chiều rộng.")]
    public string Width { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Chiều cao.")]
    public string Height { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Số chỗ ngồi.")]
    public string Sit { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Số chỗ đứng.")]
    public string Stand { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Số chỗ nằm.")]
    public string Lie { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Khả năng chở hàng.")]
    public string Goods { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Ngày hết hạn.")]
    public DateOnly ValidUntil { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Biển số xe.")]
    public string LicensePlate { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Ngày đăng ký đầu tiên.")]
    public DateOnly DayOfFirstRegistration { get; set; }
    
}