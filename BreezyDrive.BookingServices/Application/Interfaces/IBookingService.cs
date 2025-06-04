using BreezyDrive.BookingServices.Application.Dto.Requests;
using BreezyDrive.BookingServices.Application.Dto.Responses;

namespace BreezyDrive.BookingServices.Application.Interfaces;

public interface IBookingService
{
    Task<IEnumerable<BookingResponse>> GetAllBookingsAsync ();
    Task<BookingResponse> GetBookingByIdAsync (Guid bookingId);
    Task<BookingResponse> CreateBookingAsync (BookingRequest request);
    Task<BookingResponse> UpdateBookingAsync (Guid bookingId, BookingRequest request);
    Task<bool> DeleteBookingAsync (Guid bookingId);
}