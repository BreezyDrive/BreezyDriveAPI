using AutoMapper;
using BreezyDrive.BookingServices.Application.Interfaces;
using BreezyDrive.BookingServices.Domain.Entities;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;

namespace BreezyDrive.BookingServices.Application.Services;

public class BookingScheduleService(IMongoUnitOfWork unitOfWork, IMapper mapper) :  IBookingScheduleService
{
    private readonly IMongoRepository<BookingSchedule> _bookingScheduleRepository = unitOfWork.Repository<BookingSchedule>("BookingSchedules");
    
    public Task<IEnumerable<BookingSchedule>> GetAllBookingSchedulesAsync()
    {
        return _bookingScheduleRepository.GetAllAsync();

    }

    public async Task<BookingSchedule> GetBookingScheduleByIdAsync(Guid id)
    {
        var bookingSchedule = await _bookingScheduleRepository.GetByIdAsync(id);
        if (bookingSchedule == null) throw new CustomExceptions.DataNotFoundException("Booking Schedule not found");
        return bookingSchedule;
    }

    public async Task<BookingSchedule> CreateBookingScheduleAsync(BookingSchedule scheduleRequest)
    {
        await _bookingScheduleRepository.InsertAsync(scheduleRequest);
        return scheduleRequest;
    }

    public async Task<IEnumerable<BookingSchedule>> GenerateBookingSchedules(Booking booking)
    {
        var schedules = new List<BookingSchedule>();

        for (var date = booking.StartDate; date <= booking.EndDate; date = date.AddDays(1))
        {
            var schedule = new BookingSchedule
            {
                CarId = booking.CarId,
                BookingId = booking.Id,
                Date = date,
            };

            //gọi hàm create booking schedule
            var created = await CreateBookingScheduleAsync(schedule);
            schedules.Add(created);
        }

        return schedules;
    }


    public async Task<BookingSchedule> UpdateBookingScheduleAsync(Guid bookingScheduleId ,BookingSchedule scheduleRequest)
    {
        var bookingSchedule = await _bookingScheduleRepository.GetByIdAsync(bookingScheduleId);
        if (bookingSchedule == null) throw new CustomExceptions.DataNotFoundException("Booking Schedule not found");
        
        await _bookingScheduleRepository.UpdateAsync(bookingScheduleId, scheduleRequest);
        return scheduleRequest;
    }

    public async Task<bool> DeleteBookingScheduleAsync(Guid id)
    {
        var bookingSchedule = _bookingScheduleRepository.GetByIdAsync(id);
        if (bookingSchedule == null) throw new CustomExceptions.DataNotFoundException("Booking Schedule not found");
        
        await _bookingScheduleRepository.DeleteAsync(bookingSchedule.Id);
        
        return true;
    }
}