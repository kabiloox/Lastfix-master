namespace Electronique_Labo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adddatetimeintablexp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Expiriments", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Expiriments", "DateTime");
        }
    }
}
