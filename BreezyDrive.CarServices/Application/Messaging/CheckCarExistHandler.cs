using BreezyDrive.CarServices.Application.Interfaces;
using BreezyDrive.CommonService.Infrastuctures.Messaging;
using Library.EventContracts.Events.CarEvent.Request;
using Library.EventContracts.Events.CommonResponse;

namespace BreezyDrive.CarServices.Application.Messaging
{
    public class CheckCarExistHandler : IMessageHandler<CheckCarExistRequestEvent, EventSuccessResponse>
    {
        private readonly ICarService _carServices;

        public CheckCarExistHandler(ICarService carServices)
        {
            _carServices = carServices;
        }

        public async Task<EventSuccessResponse> HandleMessageAsync(CheckCarExistRequestEvent message)
        {
            return new EventSuccessResponse { IsSuccess = await _carServices.CheckCarExist(message.CarId) };
        }
    }
}
