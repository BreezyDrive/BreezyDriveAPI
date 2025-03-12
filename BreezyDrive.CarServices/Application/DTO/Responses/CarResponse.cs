using BreezyDrive.CarServices.Domain.Entities;
using BreezyDrive.CarServices.Domain.Enums;
using BreezyDrive.Common.Application.Mapper;

namespace BreezyDrive.CarServices.Application.DTO.Responses;

public class CarResponse : IMapFrom<Cars>
{
    public Guid UserId { get; set; }

    public Guid CarModelId { get; set; }

    public string? CarAvatar { get; set; }

    public string? FrontImage { get; set; }

    public string? BackImage { get; set; }

    public string? LeftImage { get; set; }

    public string? RightImage { get; set; }

    public  TransmissionTypeEnum TransmissionType { get; set; }

    public required string FuelType { get; set; }

    public int FuelConsumption { get; set; }

    public int Seat { get; set; }

    public required string Location { get; set; }

    public string? Description { get; set; }

    public DateOnly DayOfRegistration { get; set; }

    public bool IsDropOf { get; set; }

    public int FeePerKm { get; set; }

    public int AvailableZone { get; set; }

    public int NumberOfReservation { get; set; }

    public double PricePerDay { get; set; }
}