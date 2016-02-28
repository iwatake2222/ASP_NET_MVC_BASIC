namespace MyWebAppMVC.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ProductID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ProductID");
            AddForeignKey("dbo.Orders", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ProductID", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "ProductID" });
            DropColumn("dbo.Orders", "ProductID");
        }
    }
}
