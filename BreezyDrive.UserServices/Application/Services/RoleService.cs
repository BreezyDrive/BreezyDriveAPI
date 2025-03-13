using AutoMapper;
using BreezyDrive.CommonService.Domain.Interfaces;
using BreezyDrive.CommonService.Domain.Exceptions;
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
            if (!roles.Any())
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy role nào.");
            }

            var roleResponses = _mapper.Map<List<RoleResponse>>(roles);

            return roleResponses;
        }

        public async Task<RoleResponse> GetRoleByGuid(Guid id)
        {
            var role = _unitOfWork.Repository<Roles>().GetById(id);
            if (role == null)
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy role với Guid này.");
            }

            var roleResponse = _mapper.Map<RoleResponse>(role);

            return roleResponse;

        }

        public async Task<RoleResponse> GetRoleByName(string name)
        {
            var role = _unitOfWork.Repository<Roles>().Get(r => r.Name == name).FirstOrDefault();
            if (role == null)
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy tên role này.");
            }

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

        public async Task<bool> UpdateRole(Guid id, RoleRequest roleRequest)
        {
            var existingRole = _unitOfWork.Repository<Roles>().GetById(id);
            if (existingRole == null)
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy role với Guid này.");
            }

            return true;
        }

        public async Task<bool> DeleteRoleByGuid(Guid id)
        {
            var existingRole = _unitOfWork.Repository<Roles>().GetById(id);
            if (existingRole == null)
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy id của role này.");
            }

            _unitOfWork.Repository<Roles>().Delete(existingRole);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteRoleByName(string name)
        {
            var existingRole = _unitOfWork.Repository<Roles>().Get(er => er.Name == name).FirstOrDefault();
            if (existingRole == null)
            {
                throw new CustomExceptions.DataNotFoundException("Không tìm thấy id của role này.");
            }

            _unitOfWork.Repository<Roles>().Delete(existingRole);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}
