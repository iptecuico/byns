using AutoMapper;
using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Domain;

namespace Codetecuico.Byns.Api.Mappings
{
    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<Item, ItemModel>()
                .ForMember(to => to.Username, from => from.MapFrom(m => m.User.Username));
        }
    }
}