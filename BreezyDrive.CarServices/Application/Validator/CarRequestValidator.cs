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
            .NotEmpty().WithMessage("Vui lòng cung cấp UserId.")
            .Must(g => g != Guid.Empty).WithMessage("UserId không được là Guid trống.")
            .Must(UserExists).WithMessage("UserId không tồn tại.");

        RuleFor(x => x.CarModelId)
            .NotEmpty().WithMessage("Vui lòng cung cấp CarModelId.")
            .Must(g => g != Guid.Empty).WithMessage("CarModelId không được là Guid trống.");

        RuleFor(x => x.CarAvatar)
            .Must(IsAValidUrl).WithMessage("Ảnh đại diện xe phải là một URL hợp lệ.")
            .When(x => !string.IsNullOrEmpty(x.CarAvatar));

        RuleFor(x => x.TransmissionType)
            .IsInEnum().WithMessage("Loại truyền động không hợp lệ.");

        RuleFor(x => x.FuelType)
            .MaximumLength(50).WithMessage("Loại nhiên liệu không được vượt quá 50 ký tự.");

        RuleFor(x => x.FuelConsumption)
            .GreaterThan(0).WithMessage("Mức tiêu thụ nhiên liệu phải lớn hơn 0.");

        RuleFor(x => x.Seat)
            .GreaterThan(0).WithMessage("Số ghế phải lớn hơn 0.")
            .LessThanOrEqualTo(10).WithMessage("Số ghế không được vượt quá 10.");

        RuleFor(x => x.Location)
            .MaximumLength(200).WithMessage("Địa điểm không được vượt quá 200 ký tự.");

        RuleFor(x => x.DayOfRegistration)
            .Must(IsValidDate).WithMessage("Ngày đăng ký không hợp lệ.");

// Nếu IsDropOf là false, thì FeePerKm và AvailableZone phải bằng 0
        When(x => !x.IsDropOf, () =>
        {
            RuleFor(x => x.FeePerKm)
                .Equal(0).WithMessage("Phí mỗi km phải bằng 0 khi IsDropOf là false.");
            RuleFor(x => x.AvailableZone)
                .Equal(0).WithMessage("Khu vực có sẵn phải bằng 0 khi IsDropOf là false.");
        });

        RuleFor(x => x.FeePerKm)
            .GreaterThanOrEqualTo(0).WithMessage("Phí mỗi km phải là giá trị không âm.");

        RuleFor(x => x.AvailableZone)
            .GreaterThanOrEqualTo(0).WithMessage("Khu vực có sẵn phải là giá trị không âm.");

        RuleFor(x => x.PricePerDay)
            .GreaterThan(0).WithMessage("Giá mỗi ngày phải lớn hơn 0.");
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