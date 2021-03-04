namespace Mishutki.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Rating", c => c.Int(nullable: false, defaultValue: 0));
        }

        public override void Down()
        {
            DropColumn("dbo.Posts", "Rating");
        }
    }
}
