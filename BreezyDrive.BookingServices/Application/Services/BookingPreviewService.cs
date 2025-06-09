using BreezyDrive.BookingServices.Application.Dto.Requests;
using BreezyDrive.BookingServices.Application.Dto.Responses;
using BreezyDrive.BookingServices.Application.Interfaces;
using Library.EventContracts.Events.CarEvent.Request;
using Library.EventContracts.Events.CarEvent.Response;
using MassTransit;

namespace BreezyDrive.BookingServices.Application.Services;

public class BookingPreviewService(
    IExistenceCheckerService existenceCheckerService,
    IRequestClient<GetCarInformationRequestEvent> carInformationRequestClient) : IBookingPreviewService
{
    private readonly IExistenceCheckerService _existenceCheckerService = existenceCheckerService;

    private readonly IRequestClient<GetCarInformationRequestEvent> _carInformationRequestClient =
        carInformationRequestClient;


    public async Task<BookingPreviewResponse> CalculateBooking(BookingPreviewRequest request)
    {
        var carResponse  = await _carInformationRequestClient.GetResponse<GetCarInformationResponseEvent>(
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