using System.ComponentModel.DataAnnotations;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarRegistrationRequest : IMapFrom<CarRegistrations>
{
    [Required(ErrorMessage = "CarId is required")]
    public Guid CarId { get; set; }
    
    [Required(ErrorMessage = "Image is required")]
    public string Image { get; set; }

    [Required(ErrorMessage = "VehicleIdentificationNumber is required")]
    public string VehicleIdentificationNumber { get; set; }

    [Required(ErrorMessage = "EngineIdentificationNumber is required")]
    public string EngineIdentificationNumber { get; set; }
    
    [Required(ErrorMessage = "Capacity is required")]
    public string Capacity { get; set; }

    [Required(ErrorMessage = "Color is required")]
    public string Color { get; set; }

    [Required(ErrorMessage = "HoursePower is required")]
    public string HoursePower { get; set; }

    [Required(ErrorMessage = "YearOfManufacture is required")]
    public string YearOfManufacture { get; set; }

    [Required(ErrorMessage = "EmptyWeight is required")]
    public string EmptyWeight { get; set; }

    [Required(ErrorMessage = "Length is required")]
    public string Length { get; set; }

    [Required(ErrorMessage = "Width is required")]
    public string Width { get; set; }

    [Required(ErrorMessage = "Height is required")]
    public string Height { get; set; }

    [Required(ErrorMessage = "Sit is required")]
    public string Sit { get; set; }

    [Required(ErrorMessage = "Stand is required")]
    public string Stand { get; set; }

    [Required(ErrorMessage = "Lie is required")]
    public string Lie { get; set; }

    [Required(ErrorMessage = "Goods is required")]
    public string Goods { get; set; }

    [Required(ErrorMessage = "ValidUntil is required")]
    public DateOnly ValidUntil { get; set; }

    [Required(ErrorMessage = "LicensePlate is required")]
    public string LicensePlate { get; set; }

    [Required(ErrorMessage = "DayOfFirstRegistration is required")]
    public DateOnly DayOfFirstRegistration { get; set; }
}