using AutoMapper;

namespace Codetecuico.Byns.Api.Mappings
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToModelMappingProfile>();
                x.AddProfile<ModelToDomainMappingProfile>();
            });
        }
    }
}