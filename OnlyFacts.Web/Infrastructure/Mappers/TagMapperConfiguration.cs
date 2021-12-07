using OnlyFacts.Web.Data;
using OnlyFacts.Web.Infrastructure.Mappers.Base;
using OnlyFacts.Web.ViewModels;

namespace OnlyFacts.Web.Infrastructure.Mappers
{
    public class TagMapperConfiguration : MapperConfigurationBase
    {
        public TagMapperConfiguration()
        {
            CreateMap<Tag, TagViewModel>();
            CreateMap<Tag, TagUpdateViewModel>();
            CreateMap<TagUpdateViewModel, Tag>()
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.Facts, o => o.Ignore());
        }
    }
}