namespace ConsoleApp4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CatId = c.Int(nullable: false),
                        Name = c.String(),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Parent_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OfferId = c.Int(nullable: false),
                        Author = c.String(),
                        Title = c.String(),
                        Publisher = c.String(),
                        Year = c.String(),
                        ISBN = c.String(),
                        Description = c.String(),
                        Pages = c.String(),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offers", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Parent_Id", "dbo.Categories");
            DropIndex("dbo.Offers", new[] { "Category_Id" });
            DropIndex("dbo.Categories", new[] { "Parent_Id" });
            DropTable("dbo.Offers");
            DropTable("dbo.Categories");
        }
    }
}
