namespace Do_An.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Quyen", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Quyen", c => c.Boolean(nullable: false));
        }
    }
}
