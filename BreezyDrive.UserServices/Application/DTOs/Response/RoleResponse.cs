using AutoMapper;
using BreezyDrive.Common.Application.Mapper;
using BreezyDrive.UserServices.Domain.Entities;

namespace BreezyDrive.UserServices.Application.DTOs.Response
{
    public class RoleResponse : IMapFrom<Roles>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Roles, RoleResponse>();
        }
    }
}
