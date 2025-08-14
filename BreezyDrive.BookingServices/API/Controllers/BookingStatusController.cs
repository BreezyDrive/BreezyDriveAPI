using BreezyDrive.BookingServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.BookingServices.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingStatusController
    (
        IBookingStatusHandler bookingStatusHandler
    ) : BaseController
{
    [HttpPost("AcceptBookingAsync/{bookingId}")]
    public async Task<IActionResult> AcceptBookingAsync(Guid bookingId)
    {

        var acceptBooking = await bookingStatusHandler.AcceptBookingAsync(bookingId);
        return CustomResult("Success", acceptBooking);
    }
    
    [HttpPost("RejectBookingAsync/{bookingId}")]
    public async Task<IActionResult> RejectBookingAsync(Guid bookingId)
    {

        var acceptBooking = await bookingStatusHandler.RejectBookingAsync(bookingId);
        return CustomResult("Success", acceptBooking);
    }
}