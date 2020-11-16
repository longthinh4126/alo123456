using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Do_An.Models
{
        public class ProductContext : DbContext
        {
            public ProductContext()
            {
                SqlConnectionStringBuilder sqlb = new SqlConnectionStringBuilder();
                sqlb.DataSource = "DESKTOP-1E3KRN0\\SQLEXPRESS";
                sqlb.InitialCatalog = "DoAnContext";
                sqlb.IntegratedSecurity = true;
                this.Database.Connection.ConnectionString = sqlb.ConnectionString;
            }
            public DbSet<Product> Products { get; set; }
            public DbSet<ProductDetail> ProductDetails { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderDetail> OrderDetails { get; set; }
            public DbSet<Promotion> Promotions { get; set; }
            public DbSet<PromotionDetail> PromotionDetails { get; set; }
            public DbSet<Customer> Customers { get; set; }
            public DbSet<Type> Types { get; set; }
            public DbSet<Brand> Brands { get; set; }
            public DbSet<Info> Infos { get; set; }
        }
        public class Type
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public virtual ICollection<Product> Products { get; set; }
        }
        public class Brand
        {
            [Key]
            public int ID { get; set; }
            [MaxLength(30)]
            public string Name { get; set; }
            public virtual ICollection<Product> Products { get; set; }
        }
        public class ProductDetail
        {
            [Key]
            public int ID { get; set; }
            public Nullable<int> ProductID { get; set; }
            public virtual Product Product { get; set; }
            [Range(1,100)]
            public int TinhTrang { get; set; }
            public string BaoHanh { get; set; }
            public string MauSac { get; set; }
            public Nullable<int> TypeID { get; set; }
            public virtual Type Type { get; set; }
            public string MoTa { get; set; }
        }
        public class Product
        {
            [Key]
            public int ID { get; set; }
            [MaxLength(50)]
            public string Name { get; set; }
            public bool IsShow { get; set; }
            [Range(1, 9999999999,ErrorMessage ="Nhap lon hon 1")]
            public int Gia { get; set; }
            public string Image { get; set; }
            public int SaleCount { get; set; }
            public Nullable<int> BrandID { get; set; }
            public virtual Brand Brand { get; set; }
            public Nullable<int> TypeID { get; set; }
            public virtual Type Type { get; set; }
            [NotMapped]
            public HttpPostedFileBase ImageUpload { get; set; }
            public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        }
        public class Order
        {
            [Key]
            public int ID { get; set; }
            public DateTime NgayTao { get; set; }
            [MaxLength(300)]
            public string Note { get; set; }
            public string TenKH { get; set; }
            public string DiaChiKH { get; set; }
            public string SDTKH { get; set; }
            public double GiamGia { get; set; }
            public int TrangThai { get; set; }
            public int TongGia { get; set; }
            public Nullable<int> ChiTietID { get; set; }
            public virtual ProductDetail ProductDetail { get; set; }
            public Nullable<int> CusID { get; set; }
            public virtual Customer Customer { get; set; }
            public ICollection<OrderDetail> OrderDetails { get; set; }
        }
        public class OrderDetail
        {
            [Key]
            public int ID { get; set; }
            public Nullable<int> ProductID { get; set; }
            public virtual Product Product { get; set; }
            public Nullable<int> OrderID { get; set; }
            public virtual Order Order { get; set; }
            public int SoLuong { get; set; }
            public int Gia { get; set; }
            public string GhiChu { get; set; }
            public Nullable<double> GiamGia { get; set; }
            public int TongCong { get; set; }
            public Promotion Promotion { get; set; }
        }
        public class Customer
        {
            [Key]
            public int ID { get; set; }
            [MaxLength(100)]
            public string Name { get; set; }
            [Required]
            public string UserName { get; set; }
            [Required]
            [MinLength(8)]
            public string Password { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            [MaxLength(11)]
            [MinLength(10)]
            public string SDT { get; set; }
            public string DiaChi { get; set; }
            public string Quyen { get; set; }
            public ICollection<Order> Orders { get; set; }
        }
        public class Promotion
        {
            [Key]
            public int ID { get; set; }
            [MaxLength(30)]
            public string Name { get; set; }
            public DateTime starDate { get; set; }
            public DateTime endDate { get; set; }
            public bool TrangThai { get; set; }
            public double GiamGia { get; set; }
            public ICollection<Order> Orders { get; set; }
            public ICollection<PromotionDetail> PromotionDetails { get; set; }
        }
        public class PromotionDetail
        {
            [Key]
            public int ID { get; set; }
            public Nullable<int> PromotionID { get; set; }
            public virtual Promotion Promotion { get; set; }
            public Nullable<int> ProductID { get; set; }
            public virtual Product Products { get; set; }
        }
        public class Info
        {
            [Key]
            public int ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }
}