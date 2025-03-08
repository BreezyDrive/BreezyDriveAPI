using BreezyDrive.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreezyDrive.Cars.Entities
{
    [Table("CarRegistrations")]
    public class CarRegistrations : BaseEntities
    {
        public Guid CarId { get; set; }
    
        public required string Image { get; set; }
    
        public required string VehicleIdentificationNumber { get; set; }
    
        public required string EngineIdentificationNumber { get; set; }
    
        public required string Capacity { get; set; }
    
        public required string Color { get; set; }
    
        public required string HoursePower { get; set; }
    
        public required string YearOfManufacture { get; set; }
    
        public required string EmptyWeight { get; set; }
        public required string Length { get; set; }
    
        public required string Width { get; set; }
        public required string Height { get; set; }
        public required string Sit { get; set; }
        public required string Stand { get; set; }
        public required string Lie { get; set; }
        public required string Goods { get; set; }
    
        public required DateOnly ValidUntil { get; set; }
    
        public required string LicensePlate { get; set; }
    
        public required DateOnly DayOfFirstRegistration { get; set; }
    
        [ForeignKey("CarId")]
        public virtual required Cars Car { get; set; }
    
    
    }
}

