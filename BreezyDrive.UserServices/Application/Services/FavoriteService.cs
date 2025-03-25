using AutoMapper;
using BreezyDrive.AuthenticationServices.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.UserServices.Application.DTOs.Response;
using BreezyDrive.UserServices.Application.Interfaces;
using BreezyDrive.UserServices.Domain.Entities;
using Library.EventContracts.Events.CarEvent.Request;
using Library.EventContracts.Events.CommonResponse;
using MassTransit;

namespace BreezyDrive.UserServices.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthentication _authentication;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRequestClient<CheckCarExistRequestEvent> _carExistClient;

        public FavoriteService(IUnitOfWork unitOfWork, IMapper mapper, IAuthentication authentication, IHttpContextAccessor httpContextAccessor,
                                IRequestClient<CheckCarExistRequestEvent> carExistClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authentication = authentication;
            _httpContextAccessor = httpContextAccessor;
            _carExistClient = carExistClient;
        }

        public async Task<List<FavoriteResponse>> GetAllCarFavorite()
        {
            var userId = _authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            var carFavorite = _unitOfWork.Repository<Favorites>().GetAll().Where(x => x.UserId == userId);
            if (!carFavorite.Any())
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy xe yêu thích của người dùng.");
            }

            var carFavoriteResponse = _mapper.Map<List<FavoriteResponse>>(carFavorite);
            return carFavoriteResponse;
        }

        public async Task<bool> AddFavorite(Guid carId)
        {
            var userId = _authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            var response = await _carExistClient.GetResponse<EventSuccessResponse>(
                new CheckCarExistRequestEvent
                {
                    CarId = carId
                });

            if (response.Message.IsSuccess == false)
            {
                throw new CustomExceptions.InvalidDataException("Không tìm thấy xe.");
            }

            var favorite = new Favorites
            {
                UserId = userId,
                CarId = carId
            };

            _unitOfWork.Repository<Favorites>().Insert(favorite);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> RemoveFavorite(Guid carId)
        {
            var userId = _authentication.GetUserIdFromHttpContext(_httpContextAccessor.HttpContext);
            var favorite = _unitOfWork.Repository<Favorites>().GetAll().FirstOrDefault(x => x.UserId == userId && x.CarId == carId);
            if (favorite == null)
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy xe yêu thích.");
            }

            _unitOfWork.Repository<Favorites>().Delete(favorite);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}
