using AutoMapper;
using TaskTracker.Api.Dtos;
using TaskTracker.Models.Models;

namespace TaskTracker.Api.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
