using AutoMapper;
using BreezyDrive.BookingServices.Application.Interfaces;
using BreezyDrive.BookingServices.Domain.Entities;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;

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

    public async Task<bool> CheckScheduleExistsAsync(Guid carId, DateOnly startDate, DateOnly endDate)
    {
        var bookingSchedule = await _bookingScheduleRepository
            .GetAsync(filter: x => x.CarId == carId && x.Date >= startDate && x.Date <= endDate);
        if (!bookingSchedule.IsNullOrEmpty())
        {
            throw new CustomExceptions.InvalidDataException("A schedule for this car with the same dates already exists");
        }
        return false;
    }
 
    public async Task<IEnumerable<Guid>> GetCarIdsAlreadyScheduled(DateOnly startDate, DateOnly endDate)
    {
        var schedules = await _bookingScheduleRepository.GetAsync(filter:x => x.Date >= startDate && x.Date <= endDate);
        var carIds = schedules
            .Select(x => x.CarId)
            .Distinct();
        return carIds;
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
        var bookingSchedule = await _bookingScheduleRepository.GetByIdAsync(id);
        if (bookingSchedule == null) throw new CustomExceptions.DataNotFoundException("Booking Schedule not found");
        
        await _bookingScheduleRepository.DeleteAsync(bookingSchedule.Id);
        
        return true;
    }
}