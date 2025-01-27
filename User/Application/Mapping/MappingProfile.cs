using AutoMapper;
using User.Application.DTOS;
using User.Core.Entities;

namespace User.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User.Core.Entities.User, UserDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<CreateUserDTO,User.Core.Entities.User>();
            CreateMap<UpdatedUserDTO, User.Core.Entities.User>();

            // Role Mappings
            CreateMap<Role, RoleDTO>();
        }
    }
}
