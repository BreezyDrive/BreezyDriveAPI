using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using FluentValidation;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;

namespace BreezyDrive.CarServices.Application.Validator;

public class CarRatingRequestValidator : AbstractValidator<CarRatingRequest>
{
    
    private readonly IRequestClient<CheckUserExistRequest> _userClient;
    private readonly ICarService _carService;

    public CarRatingRequestValidator(IRequestClient<CheckUserExistRequest> userClient, ICarService carService)
    {
        _userClient = userClient;
        _carService = carService;

        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.")
            .Must(g => g != Guid.Empty).WithMessage("UserId cannot be an empty Guid.")
            .Must(UserExists).WithMessage("User does not exist.");

        RuleFor(x => x.CarId).NotEmpty().WithMessage("CarId is required.")
            .Must(g => g != Guid.Empty).WithMessage("CarId cannot be an empty Guid.")
            .Must(CarExists).WithMessage("Car does not exist.");

    }
    
    private bool UserExists(Guid userId)
    {
        var response = _userClient.GetResponse<CheckUserExistResponse>(new CheckUserExistRequest { UserId = userId });
        return response.Result.Message.IsUserExists;
    }

    private bool CarExists(Guid carId)
    {
        return _carService.IsCarExists(carId);
    }
    
}