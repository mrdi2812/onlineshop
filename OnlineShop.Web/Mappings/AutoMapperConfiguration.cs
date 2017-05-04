using AutoMapper;
using OnlineShop.Model.Models;
using OnlineShop.Web.Models;

namespace OnlineShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Post, PostViewModel>();
                config.CreateMap<PostCategory, PostCategoryViewModel>();
                config.CreateMap<Tag, TagViewModel>();
                config.CreateMap<PostTag, PostTagViewModel>();
            });
        }
    }
}