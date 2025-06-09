namespace BreezyDrive.BookingServices.Application.Interfaces;

public interface IExistenceCheckerService
{
    bool IsUserExists(Guid userId);
    bool IsCarExists(Guid carId);
}