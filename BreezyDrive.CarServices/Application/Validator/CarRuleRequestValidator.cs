using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using FluentValidation;

namespace BreezyDrive.CarServices.Application.Validator;

public class CarRuleRequestValidator : AbstractValidator<CarRuleRequest>
{
    private readonly ICarService _carService;
    private readonly IRuleService _ruleService;
    
    public CarRuleRequestValidator(ICarService carService, IRuleService ruleService)
    {
        _carService = carService;
        _ruleService = ruleService;
        
        RuleFor(x => x.CarId).NotEmpty().WithMessage("CarId cannot be an empty Guid.")
            .Must(g => g != Guid.Empty).WithMessage("CarId cannot be an empty Guid.")
            .Must(CarExists).WithMessage("Car does not exist.");
        
        RuleFor(x => x.RuleId).NotEmpty().WithMessage("RuleId cannot be an empty Guid.")
            .Must(g => g != Guid.Empty).WithMessage("RuleId cannot be an empty Guid.")
            .Must(RuleExists).WithMessage("Car does not exist.");
            
        
    }

    private bool CarExists(Guid carId)
    {
        return _carService.IsCarExists(carId);
    }

    private bool RuleExists(Guid ruleId)
    {
        return _ruleService.IsRuleExists(ruleId);
    }
}