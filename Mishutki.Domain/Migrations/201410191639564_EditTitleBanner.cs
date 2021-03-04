namespace Mishutki.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditTitleBanner : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Banners", "Title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Banners", "Title", c => c.Int(nullable: false));
        }
    }
}
