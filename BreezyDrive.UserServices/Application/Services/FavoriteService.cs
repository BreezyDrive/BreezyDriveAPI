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
        private readonly IRequestClient<CheckCarExistRequestEvent> _carExistClient;
        private readonly ITokenService _tokenService;

        public FavoriteService(IUnitOfWork unitOfWork, IMapper mapper, IRequestClient<CheckCarExistRequestEvent> carExistClient,
                                ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _carExistClient = carExistClient;
            _tokenService = tokenService;
        }

        public async Task<List<FavoriteResponse>> GetAllCarFavorite()
        {
            var userId = await _tokenService.GetUserIdAsync();
            var carFavorite = _unitOfWork.Repository<Favorites>().GetAll().Where(x => x.UserId == userId);

            if (!carFavorite.Any())
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy xe yêu thích của người dùng.");
            }

            return _mapper.Map<List<FavoriteResponse>>(carFavorite);
        }

        public async Task<bool> AddFavorite(Guid carId)
        {
            var userId = await _tokenService.GetUserIdAsync();
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
            var userId = await _tokenService.GetUserIdAsync();
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
