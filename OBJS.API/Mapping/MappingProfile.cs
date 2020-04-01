using AutoMapper;
using OBJS.API.Models.Categories;

namespace OBJS.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /*CreateMap<Category, Category>()
                .ForMember(dest => dest.ParentID, opt => opt.MapFrom(src => src.CategoryId));
            */
        }

    }
}
