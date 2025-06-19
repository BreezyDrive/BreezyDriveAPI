using System.Reflection;
using BreezyDrive.BookingServices.Application.Dto.Requests;
using BreezyDrive.BookingServices.Application.Dto.Responses;
using BreezyDrive.BookingServices.Application.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using Library.EventContracts.Events.CarEvent.Request;
using Library.EventContracts.Events.CarEvent.Response;
using MassTransit;

namespace BreezyDrive.BookingServices.Application.Services;

public class BookingPreviewService(
    IExistenceCheckerService existenceCheckerService,
    IRequestClient<GetCarInformationRequestEvent> carInformationRequestClient) : IBookingPreviewService
{
    public async Task<BookingPreviewResponse> CalculateBooking(BookingPreviewRequest request)
    {
        var isCarExists = existenceCheckerService.IsCarExists(request.CarId);
        if (!isCarExists)
        {
            throw new CustomExceptions.DataNotFoundException("Không tìm thấy xe!");
        }
        var carResponse  = await carInformationRequestClient.GetResponse<GetCarInformationResponseEvent>(
            new GetCarInformationRequestEvent { CarId = request.CarId });

        var car = carResponse.Message;
        
        
        var response = new BookingPreviewResponse
        {
            CarId = car.CarId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            
        };
        response.TotalPrice = response.TotalDays * car.PricePerDay;
        
        return response;
    }
}