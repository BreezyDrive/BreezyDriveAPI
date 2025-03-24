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
        RuleFor(x => x.BrandId).NotEmpty().WithMessage("BrandId is required.")
            .Must(g => g != Guid.Empty).WithMessage("BrandId cannot be an empty Guid.")
            .Must(BrandExists).WithMessage("Brand does not exist.");

    }

    private bool BrandExists(Guid brandId)
    {
        return _carBrandService.IsBrandExists(brandId);
    }
}