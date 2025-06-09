using BreezyDrive.BookingServices.Application.Interfaces;
using BreezyDrive.BookingServices.Domain.Entities;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.BookingServices.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingScheduleController(IBookingScheduleService bookingScheduleService) : BaseController
{
    [HttpGet("GetAllBookingSchedules")]
    public async Task<IActionResult> GetAllBookingSchedules()
    {
        return CustomResult("Success", await bookingScheduleService.GetAllBookingSchedulesAsync());
    }

    [HttpGet("GetBookingScheduleById/{bokingScheduleId}")]
    public async Task<IActionResult> GetBookingScheduleById(Guid bookingScheduleId)
    {
        var bookingSchedule = await bookingScheduleService.GetBookingScheduleByIdAsync(bookingScheduleId);
        return CustomResult("Success", bookingSchedule);
    }

    [HttpPost("CreateBookingSchedule")]
    public async Task<IActionResult> CreateBookingSchedule([FromBody] BookingSchedule request)
    {
        var bookingSchedule = await bookingScheduleService.CreateBookingScheduleAsync(request);
        return CustomResult("Success", bookingSchedule);
    }

    [HttpPatch("UpdateBookingSchedule/{bookingScheduleId}")]
    public async Task<IActionResult> UpdateBookingSchedule([FromBody] BookingSchedule request, Guid bookingScheduleId)
    {
        var bookingSchedule = await bookingScheduleService.UpdateBookingScheduleAsync(bookingScheduleId, request);
        return CustomResult("Success", bookingSchedule);
    }

    [HttpDelete("DeleteBookingSchedule/{bookingScheduleId}")]
    public async Task<IActionResult> DeleteBookingSchedule(Guid bookingScheduleId)
    {
        var bookingSchedule = await bookingScheduleService.DeleteBookingScheduleAsync(bookingScheduleId);
        return CustomResult("Success", bookingSchedule);
    }
}