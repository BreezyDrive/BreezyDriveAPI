using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using FluentValidation;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;

namespace BreezyDrive.CarServices.Application.Validator;

public class CarRatingRequestValidator : AbstractValidator<CarRatingRequest>
{
    
    private readonly IRequestClient<CheckUserExistRequestEvent> _userClient;
    private readonly ICarService _carService;

    public CarRatingRequestValidator(IRequestClient<CheckUserExistRequestEvent> userClient, ICarService carService)
    {
        _userClient = userClient;
        _carService = carService;
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Vui lòng cung cấp UserId.")
            .Must(g => g != Guid.Empty).WithMessage("UserId không được là Guid trống.")
            .Must(UserExists).WithMessage("Người dùng không tồn tại.");

        RuleFor(x => x.CarId)
            .NotEmpty().WithMessage("Vui lòng cung cấp CarId.")
            .Must(g => g != Guid.Empty).WithMessage("CarId không được là Guid trống.")
            .Must(CarExists).WithMessage("Xe không tồn tại.");

    }
    
    private bool UserExists(Guid userId)
    {
        var response = _userClient.GetResponse<CheckUserExistResponse>(new CheckUserExistRequestEvent { UserId = userId });
        return response.Result.Message.IsUserExists;
    }

    private bool CarExists(Guid carId)
    {
        return _carService.IsCarExists(carId);
    }
    
}