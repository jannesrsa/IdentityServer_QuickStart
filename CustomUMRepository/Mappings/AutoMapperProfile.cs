using AutoMapper;
using CustomUserManagerRepository.Dto;
using CustomUserManagerRepository.Model;

namespace CustomUserManagerRepository.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}