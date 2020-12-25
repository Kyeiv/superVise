using AutoMapper;
using superVise.Entities;
using superVise.Models.Requests;
using superVise.Models.Responses;

namespace superVise.Helpers.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<RegisterRequest, User>();
            CreateMap<UpdateUserRequest, User>();
        }
    }
}