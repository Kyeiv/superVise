using AutoMapper;
using superVise.Entities;
using superVise.Models.Requests;

namespace superVise.Helpers.Mappers
{
    public class AutoMapperLocation: Profile
    {
        public AutoMapperLocation()
        {
            CreateMap<NewLocationRequest, Location>();
        }
    }
}