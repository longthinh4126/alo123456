namespace Do_An.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoAnWeb3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DiaChiKH", c => c.String());
            AddColumn("dbo.Orders", "SDTKH", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "SDTKH");
            DropColumn("dbo.Orders", "DiaChiKH");
        }
    }
}
