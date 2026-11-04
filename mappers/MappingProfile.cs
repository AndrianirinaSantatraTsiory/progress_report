using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProgressReport.DTO;
using ProgressReport.models;

namespace ProgressReport.mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
    {
        CreateMap<User, UserDto>();
        
        CreateMap<CreateUserDto, User>();
    }
    }
}