namespace BreezyDrive.BookingServices.Application.Interfaces;

public interface IBookingPermissionChecker
{
    Task EnsureUserIsCarOwnerAsync(Guid carId, Guid currentUserId);

}