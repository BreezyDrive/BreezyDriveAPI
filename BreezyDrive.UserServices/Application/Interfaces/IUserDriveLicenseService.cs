using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.DTOs.Response;

namespace BreezyDrive.UserServices.Application.Interfaces
{
    public interface IUserDriveLicenseService
    {
        Task<List<UserDriveLisenceResponse>> GetAllUserDriveLisence();
        Task<UserDriveLisenceResponse> GetUserDriveLisenceById(Guid id);
        Task<bool> RegisterLicense(RegisterLicenseRequest registerLicenseRequest);
    }
}
