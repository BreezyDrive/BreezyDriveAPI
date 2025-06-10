using BreezyDrive.BookingServices.Application.Interfaces;
using BreezyDrive.BookingServices.Domain.Entities;
using BreezyDrive.BookingServices.Domain.Enums;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using Library.EventContracts.Events.CarEvent.Request;
using Library.EventContracts.Events.CarEvent.Response;
using MassTransit;

namespace BreezyDrive.BookingServices.Application.Services;

public class BookingStatusHandler(
    IHttpContextAccessor httpContextAccessor, 
    ITokenService tokenService, 
    IMongoUnitOfWork unitOfWork,
    IBookingPermissionChecker bookingPermissionChecker,
    IBookingScheduleService bookingScheduleService)
    : IBookingStatusHandler
{
    
    private readonly IMongoRepository<Booking> _bookingRepository = unitOfWork.Repository<Booking>("Bookings");

    public async Task<bool> AcceptBookingAsync(Guid bookingId)
    {
        // Lấy userId từ token
        var currentUserId = await tokenService.GetUserIdFromHttpContext(httpContextAccessor);

        // Lấy thông tin booking
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null || booking.BookingStatus != BookingStatus.Pending)
            throw new CustomExceptions.DataNotFoundException("Không tìm thấy booking hoặc booking không đang chờ.");
        
        // Lấy thông tin xe để xác minh chủ xe
        await bookingPermissionChecker.EnsureUserIsCarOwnerAsync(booking.CarId, currentUserId);
       

        // Cập nhật trạng thái booking
        booking.BookingStatus = BookingStatus.Accepted;

        await _bookingRepository.UpdateAsync(booking.Id, booking);
        
        //Update BookingSchedule
        await bookingScheduleService.GenerateBookingSchedules(booking);
        
        return true;
    }

    public async Task<bool> RejectBookingAsync(Guid bookingId)
    {
        // Lấy userId từ token
        var currentUserId = await tokenService.GetUserIdFromHttpContext(httpContextAccessor);

        // Lấy thông tin booking
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null || booking.BookingStatus != BookingStatus.Pending)
            throw new CustomExceptions.DataNotFoundException("Không tìm thấy booking hoặc booking không đang chờ.");
        
        // Lấy thông tin xe để xác minh chủ xe
        await bookingPermissionChecker.EnsureUserIsCarOwnerAsync(booking.CarId, currentUserId);
       

        // Cập nhật trạng thái booking
        booking.BookingStatus = BookingStatus.Rejected;

        await _bookingRepository.UpdateAsync(booking.Id, booking);
        
        return true;    
    }
}