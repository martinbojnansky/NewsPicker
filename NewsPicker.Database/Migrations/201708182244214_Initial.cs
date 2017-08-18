namespace NewsPicker.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Image = c.String(),
                        Url = c.String(nullable: false),
                        UrlHash = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        EngagementCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UrlHash);
            
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(nullable: false),
                        UpdatedDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CountryId = c.Int(nullable: false),
                        Order = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Code = c.String(nullable: false, maxLength: 2),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Code, unique: true);
            
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SourceArticles",
                c => new
                    {
                        Source_Id = c.Int(nullable: false),
                        Article_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Source_Id, t.Article_Id })
                .ForeignKey("dbo.Sources", t => t.Source_Id, cascadeDelete: true)
                .ForeignKey("dbo.Articles", t => t.Article_Id, cascadeDelete: true)
                .Index(t => t.Source_Id)
                .Index(t => t.Article_Id);
            
            CreateTable(
                "dbo.CategorySources",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Source_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Source_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sources", t => t.Source_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Source_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategorySources", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.CategorySources", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Categories", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.SourceArticles", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.SourceArticles", "Source_Id", "dbo.Sources");
            DropIndex("dbo.CategorySources", new[] { "Source_Id" });
            DropIndex("dbo.CategorySources", new[] { "Category_Id" });
            DropIndex("dbo.SourceArticles", new[] { "Article_Id" });
            DropIndex("dbo.SourceArticles", new[] { "Source_Id" });
            DropIndex("dbo.Countries", new[] { "Code" });
            DropIndex("dbo.Categories", new[] { "CountryId" });
            DropIndex("dbo.Articles", new[] { "UrlHash" });
            DropTable("dbo.CategorySources");
            DropTable("dbo.SourceArticles");
            DropTable("dbo.Errors");
            DropTable("dbo.Countries");
            DropTable("dbo.Categories");
            DropTable("dbo.Sources");
            DropTable("dbo.Articles");
        }
    }
}
