using APIcv.Dto;
using APIcv.Models;
using AutoMapper;

namespace APIcv.helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CV, CVDto>();
            CreateMap<CVDto, CV>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<CVEXPORTE, CVEXPORTEDto>();
            CreateMap<CVEXPORTEDto, CVEXPORTE>();
            CreateMap<EMPLOYEE, EMPLOYEEDto>();
            CreateMap<EMPLOYEEDto, EMPLOYEE>();
        }
    }
}
