namespace BreezyDrive.BookingServices.Application.Interfaces;

public interface IBookingStatusHandler
{
    Task<bool> AcceptBookingAsync(Guid bookingId);
    Task<bool> RejectBookingAsync(Guid bookingId);
}