using System.ComponentModel.DataAnnotations;
using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CarServices.Domain.Enums;
using BreezyDrive.CommonService.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Requests;

public class CarRequest : IMapFrom<Cars>
{
    
    public Guid UserId { get; set; }
    
    public Guid CarModelId { get; set; }

    [Required(ErrorMessage = "Car Image is required")]
    public string CarAvatar { get; set; }

    public string? FrontImage { get; set; }

    public string? BackImage { get; set; }

    public string? LeftImage { get; set; }

    public string? RightImage { get; set; }

    [Required(ErrorMessage = "Car Type is required")]
    public TransmissionTypeEnum TransmissionType { get; set; }

    [Required(ErrorMessage = "Car Type is required")]
    public string FuelType { get; set; }

    [Required(ErrorMessage = "Fuel Consumption is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Fuel Consumption must be greater than 0")]
    public int FuelConsumption { get; set; }

    [Required(ErrorMessage = "Number of seats is required")]
    public int Seat { get; set; }

    [Required(ErrorMessage = "Location is required")]
    public string Location { get; set; }

    public string? Description { get; set; }

    [Required(ErrorMessage = "Day of registration is required")]
    public DateOnly DayOfRegistration { get; set; }

    [Required(ErrorMessage = "Is Drop Of required")]
    public bool IsDropOf { get; set; }
    
    public int? FeePerKm { get; set; }
    
    public int? AvailableZone { get; set; }
    
    [Required(ErrorMessage = "Price Per Day is required")]
    public double PricePerDay { get; set; }
    
}