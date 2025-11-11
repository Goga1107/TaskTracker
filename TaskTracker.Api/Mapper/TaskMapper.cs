using AutoMapper;
using TaskTracker.Api.Dtos;
using TaskTracker.Models.Models;

namespace TaskTracker.Api.Mapper
{
    public class TaskMapper : Profile
    {
        public TaskMapper() 
        {
            CreateMap<TaskItem, TaskItemDto>().ReverseMap();
            
         
        }
    }
}
