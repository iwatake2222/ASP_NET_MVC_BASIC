namespace MyWebAppMVC.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Product_ProductID", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "Product_ProductID" });
            RenameColumn(table: "dbo.Orders", name: "Product_ProductID", newName: "ProductID");
            AlterColumn("dbo.Orders", "ProductID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ProductID");
            AddForeignKey("dbo.Orders", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ProductID", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "ProductID" });
            AlterColumn("dbo.Orders", "ProductID", c => c.Int());
            RenameColumn(table: "dbo.Orders", name: "ProductID", newName: "Product_ProductID");
            CreateIndex("dbo.Orders", "Product_ProductID");
            AddForeignKey("dbo.Orders", "Product_ProductID", "dbo.Products", "ProductID");
        }
    }
}
