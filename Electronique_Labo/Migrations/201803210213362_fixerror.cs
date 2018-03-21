namespace Electronique_Labo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixerror : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conseils",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        ExpirimentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Expiriments", t => t.ExpirimentId, cascadeDelete: true)
                .Index(t => t.ExpirimentId);
            
            CreateTable(
                "dbo.Expiriments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titre = c.String(),
                        Image = c.String(),
                        But = c.String(),
                        ResultatAttendue = c.String(),
                        Notes = c.String(),
                        NiveauId = c.Int(nullable: false),
                        SecteurId = c.Int(nullable: false),
                        IdFilliere = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Niveaux", t => t.NiveauId, cascadeDelete: true)
                .ForeignKey("dbo.Secteurs", t => t.SecteurId, cascadeDelete: true)
                .Index(t => t.NiveauId)
                .Index(t => t.SecteurId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.GoogleDriveFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TiTle = c.String(),
                        FileId = c.String(),
                        Name = c.String(),
                        Size = c.Long(),
                        Version = c.Long(),
                        ExpirimentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Expiriments", t => t.ExpirimentId, cascadeDelete: true)
                .Index(t => t.ExpirimentId);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titre = c.String(),
                        Image = c.String(),
                        ExpirimentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Expiriments", t => t.ExpirimentId, cascadeDelete: true)
                .Index(t => t.ExpirimentId);
            
            CreateTable(
                "dbo.Niveaux",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Filliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Abrv = c.String(),
                        NiveauId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Niveaux", t => t.NiveauId, cascadeDelete: true)
                .Index(t => t.NiveauId);
            
            CreateTable(
                "dbo.Outils",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        ExpirimentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Expiriments", t => t.ExpirimentId, cascadeDelete: true)
                .Index(t => t.ExpirimentId);
            
            CreateTable(
                "dbo.Secteurs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.profileimgs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        image = c.String(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.profileimgs", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Expiriments", "SecteurId", "dbo.Secteurs");
            DropForeignKey("dbo.Outils", "ExpirimentId", "dbo.Expiriments");
            DropForeignKey("dbo.Filliers", "NiveauId", "dbo.Niveaux");
            DropForeignKey("dbo.Expiriments", "NiveauId", "dbo.Niveaux");
            DropForeignKey("dbo.Images", "ExpirimentId", "dbo.Expiriments");
            DropForeignKey("dbo.GoogleDriveFiles", "ExpirimentId", "dbo.Expiriments");
            DropForeignKey("dbo.Conseils", "ExpirimentId", "dbo.Expiriments");
            DropForeignKey("dbo.Expiriments", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.profileimgs", new[] { "ApplicationUserId" });
            DropIndex("dbo.Outils", new[] { "ExpirimentId" });
            DropIndex("dbo.Filliers", new[] { "NiveauId" });
            DropIndex("dbo.Images", new[] { "ExpirimentId" });
            DropIndex("dbo.GoogleDriveFiles", new[] { "ExpirimentId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Expiriments", new[] { "ApplicationUserId" });
            DropIndex("dbo.Expiriments", new[] { "SecteurId" });
            DropIndex("dbo.Expiriments", new[] { "NiveauId" });
            DropIndex("dbo.Conseils", new[] { "ExpirimentId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.profileimgs");
            DropTable("dbo.Secteurs");
            DropTable("dbo.Outils");
            DropTable("dbo.Filliers");
            DropTable("dbo.Niveaux");
            DropTable("dbo.Images");
            DropTable("dbo.GoogleDriveFiles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Expiriments");
            DropTable("dbo.Conseils");
        }
    }
}
