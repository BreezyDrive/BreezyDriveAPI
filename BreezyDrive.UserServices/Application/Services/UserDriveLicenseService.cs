using AutoMapper;
using BreezyDrive.AuthenticationServices.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Utils;
using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.DTOs.Response;
using BreezyDrive.UserServices.Application.Interfaces;
using BreezyDrive.UserServices.Domain.Entities;

namespace BreezyDrive.UserServices.Application.Services
{
    public class UserDriveLicenseService : IUserDriveLicenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthentication _authentication;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFirebaseConfiguration _firebaseConfiguration;

        public UserDriveLicenseService(IUnitOfWork unitOfWork, IMapper mapper, IAuthentication authentication, IHttpContextAccessor httpContextAccessor,
                                        IFirebaseConfiguration firebaseConfiguration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authentication = authentication;
            _httpContextAccessor = httpContextAccessor;
            _firebaseConfiguration = firebaseConfiguration;
        }

        public async Task<List<UserDriveLisenceResponse>> GetAllUserDriveLisence()
        {
            var userDriveLisence = _unitOfWork.Repository<UserDriveLicenses>().GetAll();
            if (!userDriveLisence.Any())
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy bằng lái xe của người dùng.");
            }
            var userDriveLisenceResponse = _mapper.Map<List<UserDriveLisenceResponse>>(userDriveLisence);
            return userDriveLisenceResponse;
        }

        public async Task<UserDriveLisenceResponse> GetUserDriveLisenceById(Guid id)
        {
            var userDriveLisence = _unitOfWork.Repository<UserDriveLicenses>().GetById(id);
            if (userDriveLisence == null)
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy bằng lái xe của người dùng.");
            }
            var userDriveLisenceResponse = _mapper.Map<UserDriveLisenceResponse>(userDriveLisence);
            return userDriveLisenceResponse;
        }

        public async Task<bool> RegisterLicense(RegisterLicenseRequest registerLicenseRequest)
        {
            var userId = _authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);

            var userDriveLisence = _mapper.Map<UserDriveLicenses>(registerLicenseRequest);
            userDriveLisence.Front = await _firebaseConfiguration.UploadImage(registerLicenseRequest.Front);
            _unitOfWork.Repository<UserDriveLicenses>().Insert(userDriveLisence);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }

}
