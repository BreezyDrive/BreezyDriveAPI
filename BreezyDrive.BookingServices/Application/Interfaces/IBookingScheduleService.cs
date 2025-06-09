using BreezyDrive.BookingServices.Application.Dto.Requests;
using BreezyDrive.BookingServices.Domain.Entities;

namespace BreezyDrive.BookingServices.Application.Interfaces;

public interface IBookingScheduleService
{
    Task<IEnumerable<BookingSchedule>> GetAllBookingSchedulesAsync();

    Task<BookingSchedule> GetBookingScheduleByIdAsync(Guid id);
    
    Task<BookingSchedule> CreateBookingScheduleAsync(BookingSchedule scheduleRequest);
    
    Task<BookingSchedule> UpdateBookingScheduleAsync(Guid bookingScheduleId ,BookingSchedule scheduleRequest);
    
    Task<bool> DeleteBookingScheduleAsync(Guid id);
}