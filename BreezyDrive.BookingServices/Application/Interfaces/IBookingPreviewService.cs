using BreezyDrive.BookingServices.Application.Dto.Requests;
using BreezyDrive.BookingServices.Application.Dto.Responses;

namespace BreezyDrive.BookingServices.Application.Interfaces;

public interface IBookingPreviewService
{
    public Task<BookingPreviewResponse> CalculateBooking(BookingPreviewRequest request);
}