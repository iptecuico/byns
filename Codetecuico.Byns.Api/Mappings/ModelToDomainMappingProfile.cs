using AutoMapper;
using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Data.Entity;

namespace Codetecuico.Byns.Api.Mappings
{
    public class ModelToDomainMappingProfile : Profile
    {
        public ModelToDomainMappingProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<ItemModel, Item>()
                .ForMember(to => to.User, from => from.Ignore());
        }
    }
}