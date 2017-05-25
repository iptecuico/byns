using AutoMapper;
using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Common.Domain;

namespace Codetecuico.Byns.Api.Mappings
{
    public class ModelToDomainMappingProfile : Profile
    {
        public ModelToDomainMappingProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<ItemModel, Item>()
                .ForMember(x => x.User, x => x.Ignore());
        }
    }
}