namespace NewsPicker.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Optimalization : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Articles", new[] { "SourceId" });
            CreateIndex("dbo.Articles", "SourceId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Articles", new[] { "SourceId" });
            CreateIndex("dbo.Articles", "SourceId");
        }
    }
}
