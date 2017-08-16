namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSelectUserById : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("SelectUserById",
               p => new
               {
                   userId = p.String()

               }
               ,
               @"
                select a.*
                from ApplicationUser a               
                where a.Id = userId"
               );
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.SelectUserById");
        }
    }
}
