namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOriginalPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderDetails", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "OriginalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.OrderDetails", "Quantitty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Quantitty", c => c.Int(nullable: false));
            DropColumn("dbo.Products", "OriginalPrice");
            DropColumn("dbo.OrderDetails", "Quantity");
            DropColumn("dbo.OrderDetails", "Price");
        }
    }
}
