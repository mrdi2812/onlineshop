using OnlineShop.Model.Models;
using OnlineShop.Web.Models;

namespace OnlineShop.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVM)
        {
            postCategory.ID = postCategoryVM.ID;
            postCategory.Name = postCategoryVM.Name;
            postCategory.Alias = postCategoryVM.Alias;
            postCategory.Description = postCategoryVM.Description;
            postCategory.ParentID = postCategoryVM.ParentID;
            postCategory.DisplayOrder = postCategoryVM.DisplayOrder;
            postCategory.Image = postCategoryVM.Image;
            postCategory.HomeFlag = postCategoryVM.HomeFlag;
            postCategory.CreatedDate = postCategoryVM.CreatedDate;
            postCategory.CreatedBy = postCategoryVM.CreatedBy;
            postCategory.MetaDescription = postCategoryVM.MetaDescription;
            postCategory.MetaKeyword = postCategoryVM.MetaKeyword;
            postCategory.UpdatedBy = postCategoryVM.UpdatedBy;
            postCategory.UpdatedDate = postCategoryVM.UpdatedDate;
            postCategory.Status = postCategoryVM.Status;
        }

        public static void UpdatePost(this Post post, PostViewModel postVM)
        {
            post.ID = postVM.ID;
            post.Name = postVM.Name;
            post.Alias = postVM.Alias;
            post.Description = postVM.Description;
            post.CategoryID = postVM.CategoryID;
            post.HotFlag = postVM.HotFlag;
            post.Image = postVM.Image;
            post.HomeFlag = postVM.HomeFlag;
            post.Content = postVM.Content;
            post.ViewCount = postVM.ViewCount;
            post.CreatedDate = postVM.CreatedDate;
            post.CreatedBy = postVM.CreatedBy;
            post.MetaDescription = postVM.MetaDescription;
            post.MetaKeyword = postVM.MetaKeyword;
            post.UpdatedBy = postVM.UpdatedBy;
            post.UpdatedDate = postVM.UpdatedDate;
            post.Status = postVM.Status;
        }
    }
}