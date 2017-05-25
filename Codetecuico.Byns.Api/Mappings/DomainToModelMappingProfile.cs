using AutoMapper;
using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Common.Domain;

namespace Codetecuico.Byns.Api.Mappings
{
    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<Item, ItemModel>()
                .ForMember(x => x.Username, x => x.MapFrom(m => m.User.Username));
        }
    }
}