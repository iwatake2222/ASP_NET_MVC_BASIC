namespace MyWebAppMVC.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ProductID", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "ProductID" });
            RenameColumn(table: "dbo.Orders", name: "ProductID", newName: "Product_ProductID");
            AddColumn("dbo.Orders", "ProductName", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Product_ProductID", c => c.Int());
            CreateIndex("dbo.Orders", "Product_ProductID");
            AddForeignKey("dbo.Orders", "Product_ProductID", "dbo.Products", "ProductID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Product_ProductID", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "Product_ProductID" });
            AlterColumn("dbo.Orders", "Product_ProductID", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "ProductName");
            RenameColumn(table: "dbo.Orders", name: "Product_ProductID", newName: "ProductID");
            CreateIndex("dbo.Orders", "ProductID");
            AddForeignKey("dbo.Orders", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
        }
    }
}
