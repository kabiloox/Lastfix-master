namespace Electronique_Labo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableFavoritefix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpirimentId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Expiriments", t => t.ExpirimentId, cascadeDelete: true)
                .Index(t => t.ExpirimentId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorites", "ExpirimentId", "dbo.Expiriments");
            DropForeignKey("dbo.Favorites", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Favorites", new[] { "ApplicationUserId" });
            DropIndex("dbo.Favorites", new[] { "ExpirimentId" });
            DropTable("dbo.Favorites");
        }
    }
}
