using BreezyDrive.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities
{
    [Table("Cars")]
    public class Cars
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CarId { get; set; }
    
        public Guid UserId { get; set; }
    
        public Guid CarModelId { get; set; }
    
        public string? CarAvatar { get; set; }
    
        public string? FrontImage { get; set; }
    
        public string? BackImage { get; set; }
    
        public string? LeftImage { get; set; }
    
        public string? RightImage { get; set; }
    
        public required Enum TransmissionType { get; set; }
    
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
    
        [ForeignKey("CarModelId")]
        public virtual required CarModels CarModel { get; set; }
    
    
    }
}

