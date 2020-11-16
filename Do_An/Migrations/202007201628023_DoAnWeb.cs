namespace Do_An.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoAnWeb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "TongCong", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "TongCong");
        }
    }
}
