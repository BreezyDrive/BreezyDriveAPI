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
            .NotEmpty().WithMessage("Vui lòng cung cấp CarId.")
            .Must(g => g != Guid.Empty).WithMessage("CarId không được là Guid trống.")
            .Must(CarExists).WithMessage("Xe không tồn tại.");

    }

    private bool CarExists(Guid carId)
    {
        return _carService.IsCarExists(carId);
    }
}