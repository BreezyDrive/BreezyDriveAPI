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
        
        RuleFor(x => x.CarId)
            .NotEmpty().WithMessage("CarId không được để trống.")
            .Must(g => g != Guid.Empty).WithMessage("CarId không được là Guid rỗng.")
            .Must(CarExists).WithMessage("Xe không tồn tại.");

        RuleFor(x => x.RuleId)
            .NotEmpty().WithMessage("RuleId không được để trống.")
            .Must(g => g != Guid.Empty).WithMessage("RuleId không được là Guid rỗng.")
            .Must(RuleExists).WithMessage("Quy tắc không tồn tại.");
            
        
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