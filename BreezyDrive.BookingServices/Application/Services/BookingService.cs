using AutoMapper;
using BreezyDrive.BookingServices.Application.Dto.Requests;
using BreezyDrive.BookingServices.Application.Dto.Responses;
using BreezyDrive.BookingServices.Application.Interfaces;
using BreezyDrive.BookingServices.Domain.Entities;
using BreezyDrive.BookingServices.Domain.Enums;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using MongoDB.Driver;

namespace BreezyDrive.BookingServices.Application.Services;

public class BookingService(IMongoUnitOfWork unitOfWork, IMapper mapper) : IBookingService
{
    private readonly IMongoRepository<Booking> _bookingRepository = unitOfWork.Repository<Booking>("Bookings");


    public async Task<IEnumerable<BookingResponse>> GetAllBookingsAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return mapper.Map<IEnumerable<BookingResponse>>(bookings);
    }

    public async Task<BookingResponse> GetBookingByIdAsync(Guid bookingId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null) throw new CustomExceptions.DataNotFoundException("No Booking found");
        return mapper.Map<BookingResponse>(booking);
    }

    public async Task<IEnumerable<BookingResponse>> GetAllBookingByCarIdAsync(Guid carId)
    {
        var bookingList = 
            await _bookingRepository.GetAsync(filter: filter => filter.CarId == carId, 
                modify: modify => modify.SortByDescending(booking => booking.EndDate)
        );
        
        return mapper.Map<IEnumerable<BookingResponse>>(bookingList);
        
    }

    public async Task<BookingResponse> CreateBookingAsync(BookingRequest request)
    {
        var booking = mapper.Map<Booking>(request);
        //pending status khi khởi tạo lần đầu
        booking.BookingStatus = BookingStatus.Pending;

        await _bookingRepository.InsertAsync(booking);
        return mapper.Map<BookingResponse>(booking);
    }

    public async Task<BookingResponse> UpdateBookingAsync(Guid bookingId, BookingRequest request)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null) throw new CustomExceptions.DataNotFoundException("No Booking found");

        mapper.Map(request, booking);
        await _bookingRepository.UpdateAsync(bookingId, booking);

        return mapper.Map<BookingResponse>(booking);
    }

    public async Task<bool> DeleteBookingAsync(Guid bookingId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null) throw new CustomExceptions.DataNotFoundException("No Booking found");
        await _bookingRepository.DeleteAsync(bookingId);
        return true;
    }
}