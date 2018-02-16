using AutoMapper;
using Codetecuico.Byns.Api.Models;
using Codetecuico.Byns.Domain;

namespace Codetecuico.Byns.Api.Mappings
{
    public class ModelToDomainMappingProfile : Profile
    {
        public ModelToDomainMappingProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<ItemModel, Item>()
                .ForMember(to => to.User, from => from.Ignore())
                .ForMember(to => to.Organization, from => from.Ignore());
            CreateMap<ItemForCreationModel, Item>();
            CreateMap<ItemForUpdateModel, Item>();
            CreateMap<UserForCreationModel, User>();
            CreateMap<UserForUpdateModel, User>();
        }
    }
}