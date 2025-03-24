using BreezyDrive.CarServices.Application.DTO.Requests;
using FluentValidation;
using Library.EventContracts.Events.UserEvents.Request;
using Library.EventContracts.Events.UserEvents.Response;
using MassTransit;

namespace BreezyDrive.CarServices.Application.Validator;

public class CarRequestValidator : AbstractValidator<CarRequest>
{
    
    private readonly IRequestClient<CheckUserExistRequest> _userClient;

    
    public CarRequestValidator(IRequestClient<CheckUserExistRequest> userClient)
    {
        
        _userClient = userClient;

        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .Must(g => g != Guid.Empty).WithMessage("UserId cannot be an empty Guid.")
            .Must(UserExists).WithMessage("UserId does not exist.");
        
        
        RuleFor(x => x.CarModelId)
            .NotEmpty().WithMessage("CarModelId is required.")
            .Must(g => g != Guid.Empty).WithMessage("CarModelId cannot be an empty Guid.");

        
        RuleFor(x => x.CarAvatar)
            .Must(IsAValidUrl).WithMessage("Car avatar must be a valid URL.")
            .When(x => !string.IsNullOrEmpty(x.CarAvatar));
        
        RuleFor(x => x.TransmissionType)
            .IsInEnum().WithMessage("Invalid TransmissionType.");
        
        RuleFor(x => x.FuelType)
            .NotEmpty().WithMessage("Fuel type is required.")
            .MaximumLength(50).WithMessage("Fuel type must not exceed 50 characters.");
        
        RuleFor(x => x.FuelConsumption)
            .GreaterThan(0).WithMessage("Fuel consumption must be greater than 0.");
        
        RuleFor(x => x.Seat)
            .GreaterThan(0).WithMessage("Seat count must be greater than 0.")
            .LessThanOrEqualTo(10).WithMessage("Seat count must not exceed 10.");
        
        RuleFor(x => x.Location)
            .MaximumLength(200).WithMessage("Location must not exceed 200 characters.");
        
        RuleFor(x => x.DayOfRegistration)
            .Must(IsValidDate).WithMessage("Invalid registration date.");
        
        
        // Nếu IsDropOf là false, thì FeePerKm phải bằng 0
        When(x => !x.IsDropOf, () =>
        {
            RuleFor(x => x.FeePerKm)
                .Equal(0).WithMessage("Fee per Km must be 0 when IsDropOf is false.");
            RuleFor(x => x.AvailableZone)
                .Equal(0).WithMessage("Available Zone must be 0 when IsDropOf is false.");
        });
        
        
        RuleFor(x => x.FeePerKm)
            .GreaterThanOrEqualTo(0).WithMessage("Fee per km must be a positive value.");
        
        RuleFor(x => x.AvailableZone)
            .GreaterThanOrEqualTo(0).WithMessage("Available zone must be a positive value.");
        
        RuleFor(x => x.PricePerDay)
            .GreaterThan(0).WithMessage("Price per day must be greater than 0.");
    }
    
    private bool UserExists(Guid userId)
    {
        var response = _userClient.GetResponse<CheckUserExistResponse>(new CheckUserExistRequest { UserId = userId });
        return response.Result.Message.IsUserExists;
    }
    
    private static bool IsAValidUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }

    private static bool IsValidDate(DateOnly date)
    {
        return date <= DateOnly.FromDateTime(DateTime.UtcNow);
    }
    



}