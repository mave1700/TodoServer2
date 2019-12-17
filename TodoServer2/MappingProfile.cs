using AutoMapper;
using Entities.Models;

namespace TodoServer2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Task, TaskDto>();
        }
    }
}
