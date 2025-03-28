using BreezyDrive.CommonService.Application.Mapper;
using BreezyDrive.UserServices.Domain.Entities;

namespace BreezyDrive.UserServices.Application.DTOs.Response
{
    public class FavoriteResponse : IMapFrom<Favorites>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid CarId { get; set; }
    }
}
