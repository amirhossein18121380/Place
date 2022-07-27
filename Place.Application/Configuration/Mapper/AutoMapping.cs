//using AutoMapper;
using AutoMapper;
using Place.Application.Commands;
using Place.Application.Commands.Place;
using Place.Domain.Models;

namespace Place.Application.Configuration.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserRegisterCommand>().ReverseMap().ForAllMembers(x=>x.Ignore());
            CreateMap<Domain.Models.Place, UpdatePlaceCommand>().ReverseMap().ForAllMembers(x => x.Ignore());
            CreateMap<Domain.Models.Place, AddPlaceCommand>().ReverseMap().ForAllMembers(x => x.Ignore());

            // All other mappings goes here
        }
    }
}
