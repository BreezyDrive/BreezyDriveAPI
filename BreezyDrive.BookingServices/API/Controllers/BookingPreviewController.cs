using BreezyDrive.BookingServices.Application.Dto.Requests;
using BreezyDrive.BookingServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.BookingServices.API.Controllers;

public class BookingPreviewController(IBookingPreviewService bookingPreviewService) : BaseController
{
    [HttpGet("GetBookingPreview")]
    public async Task<IActionResult> GetBookingPreviewByCarId([FromQuery] Guid carId, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        var request = new BookingPreviewRequest
        {
            CarId = carId,
            StartDate = startDate,
            EndDate = endDate
        };
        var bookingPreview = await bookingPreviewService.CalculateBooking(request);
        return CustomResult("Success", bookingPreview);
    }
    
}