namespace NewsPicker.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FacebookApiUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "EngagementCount", c => c.Int(nullable: false));
            DropColumn("dbo.Articles", "ShareCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "ShareCount", c => c.Int(nullable: false));
            DropColumn("dbo.Articles", "EngagementCount");
        }
    }
}
