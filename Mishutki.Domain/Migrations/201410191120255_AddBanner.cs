namespace Mishutki.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBanner : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BannerPlaces",
                c => new
                    {
                        BannerPlaceId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        CreatedByUser = c.String(),
                    })
                .PrimaryKey(t => t.BannerPlaceId);
            
            CreateTable(
                "dbo.Banners",
                c => new
                    {
                        BannerId = c.Int(nullable: false, identity: true),
                        Title = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        Link = c.String(),
                        Content = c.String(),
                        Order = c.Int(nullable: false, defaultValue: 0),
                        BannerPlaceId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        CreatedByUser = c.String(),
                    })
                .PrimaryKey(t => t.BannerId)
                .ForeignKey("dbo.BannerPlaces", t => t.BannerPlaceId)
                .Index(t => t.BannerPlaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Banners", "BannerPlaceId", "dbo.BannerPlaces");
            DropIndex("dbo.Banners", new[] { "BannerPlaceId" });
            DropTable("dbo.Banners");
            DropTable("dbo.BannerPlaces");
        }
    }
}
