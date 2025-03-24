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
        
        RuleFor(x => x.CarId).NotEmpty()
            .Must(g => g != Guid.Empty).WithMessage("CarId cannot be an empty Guid.")
            .Must(CarExists).WithMessage("Car does not exist.");

        RuleFor(x => x.FeatureId).NotEmpty().WithMessage("FeatureId is required.")
            .Must(g => g != Guid.Empty).WithMessage("FeatureId cannot be an empty Guid.")
            .Must(FeatureExists).WithMessage("Feature does not exist.");

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