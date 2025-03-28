using System.Data;
using BreezyDrive.CarServices.Application.DTO.Requests;
using BreezyDrive.CarServices.Application.Interfaces;
using FluentValidation;

namespace BreezyDrive.CarServices.Application.Validator;

public class CarModelRequestValidator : AbstractValidator<CarModelRequest>
{
    private readonly ICarBrandService _carBrandService;
    
    public CarModelRequestValidator(ICarBrandService carBrandService)
    {
        _carBrandService = carBrandService;
        
        RuleFor(x => x.BrandId)
            .NotEmpty().WithMessage("Vui lòng cung cấp BrandId.")
            .Must(g => g != Guid.Empty).WithMessage("BrandId không được là Guid trống.")
            .Must(BrandExists).WithMessage("Thương hiệu không tồn tại.");

    }

    private bool BrandExists(Guid brandId)
    {
        return _carBrandService.IsBrandExists(brandId);
    }
}