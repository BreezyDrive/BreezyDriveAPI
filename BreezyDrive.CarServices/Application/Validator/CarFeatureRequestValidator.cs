using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using FluentValidation;

namespace BreezyDrive.CarServices.Application.Validator;

public class CarFeatureRequestValidator : AbstractValidator<CarFeatureRequest>
{
    private readonly ICarService _carService;
    private readonly IFeatureService _featureService;
    
    public CarFeatureRequestValidator(ICarService carService, IFeatureService featureService)
    {
        _carService = carService;
        _featureService = featureService;
        
        RuleFor(x => x.CarId).NotEmpty().WithMessage("Vui lòng cung cấp CarId.")
            .Must(g => g != Guid.Empty).WithMessage("CarId không được là Guid trống.")
            .Must(CarExists).WithMessage("Xe không tồn tại.");

        RuleFor(x => x.FeatureId)
            .NotEmpty().WithMessage("FeatureId là bắt buộc.")
            .Must(g => g != Guid.Empty).WithMessage("FeatureId không được là Guid trống.")
            .Must(FeatureExists).WithMessage("Tính năng không tồn tại.");

    }

    private bool CarExists(Guid carId)
    {
        return _carService.IsCarExists(carId);
    }
    
    private bool FeatureExists(Guid featureId)
    {
        return _featureService.IsFeatureExists(featureId);
    }
    
}