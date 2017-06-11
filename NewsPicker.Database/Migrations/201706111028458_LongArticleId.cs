namespace NewsPicker.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LongArticleId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Articles");
            AlterColumn("dbo.Articles", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Articles", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Articles");
            AlterColumn("dbo.Articles", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Articles", "Id");
        }
    }
}
