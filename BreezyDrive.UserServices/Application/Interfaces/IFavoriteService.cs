using AutoMapper;
using BreezyDrive.AuthenticationServices.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.UserServices.Application.DTOs.Response;

namespace BreezyDrive.UserServices.Application.Interfaces
{
    public interface IFavoriteService
    {
        Task<List<FavoriteResponse>> GetAllCarFavorite();
        Task<bool> AddFavorite(Guid carId);
        Task<bool> RemoveFavorite(Guid carId);
    }
}
