using AutoMapper;
using BreezyDrive.Common.Domain.Interfaces;
using BreezyDrive.UserServices.Application.DTOs.Request;
using BreezyDrive.UserServices.Application.DTOs.Response;
using BreezyDrive.UserServices.Application.Interfaces;
using BreezyDrive.UserServices.Domain.Entities;

namespace BreezyDrive.UserServices.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RoleResponse>> GetAllRoles()
        {
            var roles = _unitOfWork.Repository<Roles>().Get();

            var roleResponses = _mapper.Map<List<RoleResponse>>(roles);

            return roleResponses;
        }

        public async Task<RoleResponse> GetRoleByGuid(Guid id)
        {
            var role = _unitOfWork.Repository<Roles>().GetById(id);

            var roleResponse = _mapper.Map<RoleResponse>(role);

            return roleResponse;

        }

        public async Task<bool> CreateRole(RoleRequest roleRequest)
        {
            var newRole = new Roles
            {
                Name = roleRequest.Name,
            };

            _unitOfWork.Repository<Roles>().Insert(newRole);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}
