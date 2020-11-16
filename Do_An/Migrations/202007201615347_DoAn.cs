namespace Do_An.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoAn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        IsShow = c.Boolean(nullable: false),
                        Gia = c.Int(nullable: false),
                        Image = c.String(),
                        SaleCount = c.Int(nullable: false),
                        BrandID = c.Int(),
                        TypeID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Brands", t => t.BrandID)
                .ForeignKey("dbo.Types", t => t.TypeID)
                .Index(t => t.BrandID)
                .Index(t => t.TypeID);
            
            CreateTable(
                "dbo.ProductDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(),
                        TinhTrang = c.Int(nullable: false),
                        BaoHanh = c.String(),
                        MauSac = c.String(),
                        TypeID = c.Int(),
                        MoTa = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .ForeignKey("dbo.Types", t => t.TypeID)
                .Index(t => t.ProductID)
                .Index(t => t.TypeID);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        SDT = c.String(nullable: false, maxLength: 11),
                        DiaChi = c.String(),
                        Quyen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NgayTao = c.DateTime(nullable: false),
                        Note = c.String(maxLength: 300),
                        GiamGia = c.Double(nullable: false),
                        TrangThai = c.Int(nullable: false),
                        TongGia = c.Int(nullable: false),
                        ChiTietID = c.Int(),
                        CusID = c.Int(),
                        Customer_ID = c.Int(),
                        Promotion_ID = c.Int(),
                        ProductDetail_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.Customer_ID)
                .ForeignKey("dbo.Promotions", t => t.Promotion_ID)
                .ForeignKey("dbo.ProductDetails", t => t.ProductDetail_ID)
                .Index(t => t.Customer_ID)
                .Index(t => t.Promotion_ID)
                .Index(t => t.ProductDetail_ID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(),
                        OrderID = c.Int(),
                        SoLuong = c.Int(nullable: false),
                        Gia = c.Int(nullable: false),
                        GhiChu = c.String(),
                        GiamGia = c.Double(),
                        Promotion_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.OrderID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .ForeignKey("dbo.Promotions", t => t.Promotion_ID)
                .Index(t => t.ProductID)
                .Index(t => t.OrderID)
                .Index(t => t.Promotion_ID);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        starDate = c.DateTime(nullable: false),
                        endDate = c.DateTime(nullable: false),
                        TrangThai = c.Boolean(nullable: false),
                        GiamGia = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PromotionDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PromotionID = c.Int(),
                        ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID)
                .ForeignKey("dbo.Promotions", t => t.PromotionID)
                .Index(t => t.PromotionID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Infoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ProductDetail_ID", "dbo.ProductDetails");
            DropForeignKey("dbo.OrderDetails", "Promotion_ID", "dbo.Promotions");
            DropForeignKey("dbo.PromotionDetails", "PromotionID", "dbo.Promotions");
            DropForeignKey("dbo.PromotionDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Orders", "Promotion_ID", "dbo.Promotions");
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Customer_ID", "dbo.Customers");
            DropForeignKey("dbo.ProductDetails", "TypeID", "dbo.Types");
            DropForeignKey("dbo.Products", "TypeID", "dbo.Types");
            DropForeignKey("dbo.ProductDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "BrandID", "dbo.Brands");
            DropIndex("dbo.PromotionDetails", new[] { "ProductID" });
            DropIndex("dbo.PromotionDetails", new[] { "PromotionID" });
            DropIndex("dbo.OrderDetails", new[] { "Promotion_ID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropIndex("dbo.Orders", new[] { "ProductDetail_ID" });
            DropIndex("dbo.Orders", new[] { "Promotion_ID" });
            DropIndex("dbo.Orders", new[] { "Customer_ID" });
            DropIndex("dbo.ProductDetails", new[] { "TypeID" });
            DropIndex("dbo.ProductDetails", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "TypeID" });
            DropIndex("dbo.Products", new[] { "BrandID" });
            DropTable("dbo.Infoes");
            DropTable("dbo.PromotionDetails");
            DropTable("dbo.Promotions");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.Types");
            DropTable("dbo.ProductDetails");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
        }
    }
}
