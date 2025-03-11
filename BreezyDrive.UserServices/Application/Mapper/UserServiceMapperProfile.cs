using System.Reflection;
using BreezyDrive.Common.Application.Mapper;

namespace BreezyDrive.UserServices.Application.Mapper;

public class UserServiceMapperProfile : BaseAutoMapperProfile
{
    public UserServiceMapperProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }
}