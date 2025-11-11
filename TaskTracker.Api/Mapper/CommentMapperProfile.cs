using AutoMapper;
using TaskTracker.Api.Dtos;
using TaskTracker.Models.Models;

namespace TaskTracker.Api.Mapper
{
    public class CommentMapperProfile : Profile
    {
        public CommentMapperProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}
