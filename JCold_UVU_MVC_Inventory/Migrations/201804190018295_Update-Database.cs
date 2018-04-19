namespace JCold_UVU_MVC_Inventory.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Number", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Number", c => c.Int(nullable: false));
        }
    }
}
