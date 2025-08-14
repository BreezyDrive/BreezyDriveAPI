using AutoMapper;
using BreezyDrive.BookingServices.Application.Dto.Requests;
using BreezyDrive.BookingServices.Application.Dto.Responses;
using BreezyDrive.BookingServices.Application.Interfaces;
using BreezyDrive.BookingServices.Domain.Entities;
using BreezyDrive.BookingServices.Domain.Enums;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace BreezyDrive.BookingServices.Application.Services;

public class BookingService(
    IMongoUnitOfWork unitOfWork, 
    IMapper mapper,
    IBookingScheduleService bookingScheduleService,
    BookingPermissionChecker bookingPermissionChecker,
    ITokenService tokenService,
    IHttpContextAccessor httpContextAccessor) : IBookingService
{
    private readonly IMongoRepository<Booking> _bookingRepository = unitOfWork.Repository<Booking>("Bookings");


    public async Task<IEnumerable<BookingResponse>> GetAllBookingsAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        if (bookings.IsNullOrEmpty())
        {
            throw new CustomExceptions.DataNotFoundException("Không tìm thấy booking!");
        }
        return mapper.Map<IEnumerable<BookingResponse>>(bookings);
    }

    public async Task<BookingResponse> GetBookingByIdAsync(Guid bookingId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null) throw new CustomExceptions.DataNotFoundException("Không tìm thấy booking!");
        return mapper.Map<BookingResponse>(booking);
    }

    public async Task<IEnumerable<BookingResponse>> GetAllBookingByUserLoggingIn()
    {
        var userId = await tokenService.GetUserIdFromHttpContext(httpContextAccessor);
        var bookings = await _bookingRepository.GetAsync(x => x.RentUserId == userId);
        if (bookings.IsNullOrEmpty())
        {
            throw new CustomExceptions.DataNotFoundException("Không tìm thấy booking!");
        }
        return mapper.Map<IEnumerable<BookingResponse>>(bookings);
    }

    public async Task<IEnumerable<BookingResponse>> GetAllBookingByCarIdAsync(Guid carId)
    {
        //check xem có phải chủ xe hay không
        await bookingPermissionChecker.EnsureUserIsCarOwnerAsync(carId, await tokenService.GetUserIdFromHttpContext(httpContextAccessor));
        
        var bookingList =
            await _bookingRepository.GetAsync(filter: filter => filter.CarId == carId,
                modify: modify => modify.SortByDescending(booking => booking.StartDate)
            );

        if (bookingList.IsNullOrEmpty()) throw new CustomExceptions.DataNotFoundException("Không tìm thấy booking với xe này!");

        return mapper.Map<IEnumerable<BookingResponse>>(bookingList);
    }

    public async Task<BookingResponse> CreateBookingAsync(BookingRequest request)
    {        

        var userId = await tokenService.GetUserIdFromHttpContext(httpContextAccessor);

        //check xem đã có booking trong khoảng thời gian đó chưa
        //hàm đã throw luôn exception khi đã có lịch book
        await bookingScheduleService.CheckScheduleExistsAsync(request.CarId, request.StartDate, request.EndDate);
        
        var booking = mapper.Map<Booking>(request);
        booking.RentUserId = userId;
        
        //pending status khi khởi tạo lần đầu
        booking.BookingStatus = BookingStatus.Pending;

        await _bookingRepository.InsertAsync(booking);
        return mapper.Map<BookingResponse>(booking);
    }

    public async Task<BookingResponse> UpdateBookingAsync(Guid bookingId, BookingRequest request)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null) throw new CustomExceptions.DataNotFoundException("Không tìm thấy booking!");

        mapper.Map(request, booking);
        await _bookingRepository.UpdateAsync(bookingId, booking);

        return mapper.Map<BookingResponse>(booking);
    }

    public async Task<bool> DeleteBookingAsync(Guid bookingId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null) throw new CustomExceptions.DataNotFoundException("Không tìm thấy booking!");
        await _bookingRepository.DeleteAsync(bookingId);
        return true;
    }
}