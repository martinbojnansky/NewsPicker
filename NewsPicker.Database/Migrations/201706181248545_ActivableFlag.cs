namespace NewsPicker.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivableFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sources", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Countries", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Countries", "IsActive");
            DropColumn("dbo.Categories", "IsActive");
            DropColumn("dbo.Sources", "IsActive");
        }
    }
}
