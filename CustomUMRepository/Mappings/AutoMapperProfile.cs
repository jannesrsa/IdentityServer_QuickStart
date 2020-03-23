using AutoMapper;
using CustomUserManagerRepository.Dto;
using CustomUserManagerRepository.Model;
using System;
using System.Collections.Generic;
using System.Text;

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
