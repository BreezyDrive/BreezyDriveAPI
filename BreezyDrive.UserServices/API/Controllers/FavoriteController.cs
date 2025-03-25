using BreezyDrive.UserServices.Application.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreezyDrive.UserServices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : BaseController
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("GetAllCarFavorite")]
        public async Task<IActionResult> GetAllCarFavorite()
        {
            return CustomResult("Danh sách xe yêu thích:", await _favoriteService.GetAllCarFavorite());
        }

        [HttpPost("AddFavorite")]
        public async Task<IActionResult> AddFavorite(Guid carId)
        {
            return CustomResult("Thêm xe yêu thích thành công.", await _favoriteService.AddFavorite(carId));
        }

        [HttpDelete("RemoveFavorite")]
        public async Task<IActionResult> RemoveFavorite(Guid carId)
        {
            return CustomResult("Xóa xe yêu thích thành công.", await _favoriteService.RemoveFavorite(carId));
        }
    }
}
