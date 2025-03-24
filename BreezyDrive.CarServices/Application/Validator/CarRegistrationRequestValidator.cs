using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using FluentValidation;

namespace BreezyDrive.CarServices.Application.Validator;

public class CarRegistrationRequestValidator : AbstractValidator<CarRegistrationRequest>
{
    private readonly ICarService _carService;
    
    public CarRegistrationRequestValidator(ICarService carService)
    {
        _carService = carService;
        
        RuleFor(x => x.CarId)
            .NotEmpty().WithMessage("CarId is required.")
            .Must(g => g != Guid.Empty).WithMessage("CarId cannot be an empty Guid.")
            .Must(CarExists).WithMessage("Car does not exist.");

    }

    private bool CarExists(Guid carId)
    {
        return _carService.IsCarExists(carId);
    }
}