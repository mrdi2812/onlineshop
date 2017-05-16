namespace OnlineShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineShop.Data.OnlineShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OnlineShop.Data.OnlineShopDbContext context)
        {
            //CreateProductCategoryExample(context);
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new OnlineShopDbContext()));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new OnlineShopDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "tuan",
                Email = "anhtuan281290@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Đỗ Anh Tuấn"
            };
            manager.Create(user, "123456@");
            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }
            var adminUser = manager.FindByEmail("anhtuan281290@gmail.com");
            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }
        private void CreateProductCategoryExample(OnlineShop.Data.OnlineShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listproductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() { Name="Điện Tử",Alias="dien-tu",Status=true},
                    new ProductCategory() { Name="Điện Lạnh",Alias="dien-lanh",Status=true},
                    new ProductCategory() { Name="Máy Tính",Alias="may-tinh",Status=true},
                    new ProductCategory() { Name="Viễn Thông",Alias="vien-thong",Status=true},
                };
                context.ProductCategories.AddRange(listproductCategory);
                context.SaveChanges();
            }
        }
    }
}
