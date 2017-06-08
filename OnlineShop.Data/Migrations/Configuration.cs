namespace OnlineShop.Data.Migrations
{
    using Common;
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
            CreateSlide(context);
            CreatePages(context);


        }
        private void CreateUser(OnlineShopDbContext context)
        {
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new OnlineShopDbContext()));
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new OnlineShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "tuan",
            //    Email = "anhtuan281290@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Đỗ Anh Tuấn"
            //};
            //manager.Create(user, "123456@");
            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}
            //var adminUser = manager.FindByEmail("anhtuan281290@gmail.com");
            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
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
        private void CreateSlide(OnlineShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide {Name="Slide 1",DisplayOrder=1,Status=true,Url="#",Image="/Assets/client/images/bag.jpg",Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                                <span class=""on-get"">GET NOW</span>"},
                    new Slide {Name="Slide 2",DisplayOrder=1,Status=true,Url="#",Image="/Assets/client/images/bag1.jpg",Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                                < span class=""on-get"">GET NOW</span>" }
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }
        private void CreatePages(OnlineShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                var page = new Page()
                {
                    Name="Giới thiệu",
                    Alias = "gioi-thieu",
                    Content = @"But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful. Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no resultant pleasure?",
                    Status = true
                };
                context.Pages.Add(page);
                context.SaveChanges();
            }
        }
    }
}
