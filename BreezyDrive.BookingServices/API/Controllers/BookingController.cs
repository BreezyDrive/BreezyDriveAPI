using BreezyDrive.BookingServices.Application.Dto.Requests;
using BreezyDrive.BookingServices.Application.Interfaces;
using CoreApiResponse;
using Library.EventContracts.Events.CarEvent.Request;
using Library.EventContracts.Events.CarEvent.Response;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.BookingServices.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController(
    IBookingService bookingService,
    IRequestClient<CheckUserExistRequestEvent> userClient,
    IRequestClient<CheckCarExistRequestEvent> carClient) : BaseController
{
    [HttpGet("GetAllBooking")]
    public async Task<IActionResult> GetAllBooking()
    {
        return CustomResult("Success", await bookingService.GetAllBookingsAsync());
    }

    [HttpGet("GetBookingById/{bookingId}")]
    public async Task<IActionResult> GetBookingById(Guid bookingId)
    {
        return CustomResult("Success", await bookingService.GetBookingByIdAsync(bookingId));
    }

    [HttpPost("CreateBooking")]
    public async Task<IActionResult> CreateBooking([FromBody] BookingRequest bookingRequest)
    {
        var booking = await bookingService.CreateBookingAsync(bookingRequest);
        return CustomResult("Success", booking);
    }

    [HttpPatch("UpdateBooking/{bookingId}")]
    public async Task<IActionResult> UpdateBooking(Guid bookingId, [FromBody] BookingRequest bookingRequest)
    {
        var booking = await bookingService.UpdateBookingAsync(bookingId, bookingRequest);
        return CustomResult("Success", booking);
    }

    [HttpDelete("DeleteBooking/{bookingId}")]
    public async Task<IActionResult> DeleteBooking(Guid bookingId)
    {
        var isDeleted = await bookingService.DeleteBookingAsync(bookingId);
        return CustomResult("Success", isDeleted);
    }

    //test rabbitmq
    [HttpGet("CheckIfUserExist/{id}")]
    public IActionResult CheckIfUserExist(Guid id)
    {
        var response = userClient.GetResponse<CheckUserExistResponse>(
            new CheckUserExistRequestEvent { UserId = id });

        return CustomResult("Success", response.Result.Message);
    }

    //test rabbitmq
    [HttpGet("CheckIfCarExist/{id}")]
    public IActionResult CheckIfCarExist(Guid id)
    {
        var response = carClient.GetResponse<CheckCarExistResponseEvent>(
            new CheckCarExistRequestEvent { CarId = id });

        return CustomResult("Success", response.Result.Message);
    }
}