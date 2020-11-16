﻿namespace Do_An.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoAnWeb2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TenKH", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TenKH");
        }
    }
}
