namespace Mishutki.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldStatusTypeId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "StatusTypeId", c => c.Int(nullable: false, defaultValue: 119));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "StatusTypeId");
        }
    }
}
