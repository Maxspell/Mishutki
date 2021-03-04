namespace Mishutki.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostRatings",
                c => new
                    {
                        PostRatingId = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Like = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PostRatingId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostRatings", "PostId", "dbo.Posts");
            DropIndex("dbo.PostRatings", new[] { "PostId" });
            DropTable("dbo.PostRatings");
        }
    }
}
