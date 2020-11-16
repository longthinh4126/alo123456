using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Do_An.Models;

namespace Do_An.Models
{
    public class QuanLy
    {
        #region Product
        public List<Product> RefProductForView()
        {
            using (ProductContext _db = new ProductContext())
            {
                return _db.Products.Where(item => item.IsShow == true).ToList();
            }
        }
        //them sp
        public void AddProduct(Product p)
        {
            using (ProductContext _db = new ProductContext())
            {
                Product obj = _db.Products.Where(x => x.ID == p.ID).FirstOrDefault();
                if (obj != null) return;
                _db.Products.Add(p);
                _db.SaveChanges();
            }
        }
        public void UpdateProduct(Product p)
        {
            using (ProductContext _db = new ProductContext())
            {
                var obj = _db.Products.Where(x => x.ID == p.ID).FirstOrDefault();
                ChangeProductData(obj, p);
                _db.SaveChanges();
            }
        }
        public void DeleteProduct(int id)
        {
            using (ProductContext _db = new ProductContext())
            {
                Product p = _db.Products.Find(id);
                _db.Entry(p).State = EntityState.Deleted;
                _db.SaveChanges();
                ProductDetail d = _db.ProductDetails.Find(id);
                _db.Entry(d).State = EntityState.Deleted;
                _db.SaveChanges();
            }
        }
        public List<Product> GetProductDiscount(int id)
        {
            using (ProductContext _db = new ProductContext())
            {
                PromotionDetail pd = _db.PromotionDetails.Find(id);
                return _db.Products.Where(item => item.ID == id).ToList();
            }
        }
        public void ChangeProductData(Product A, Product B)
        {
            A.Name = B.Name;
            A.IsShow = B.IsShow;
            A.Gia = B.Gia;
            A.ImageUpload = B.ImageUpload;
            A.TypeID = B.TypeID;
            A.BrandID = B.BrandID;
        }
        public void ChangeDetailData(ProductDetail A,ProductDetail B)
        {
            A.TinhTrang = B.TinhTrang;
            A.MauSac = B.MauSac;
            A.BaoHanh = B.BaoHanh;
            A.TypeID = B.TypeID;
            A.MoTa = B.MoTa;
        }
        #endregion
        //them type
        #region Type
        public List<Type> RefTypeForView()
        {
            using (ProductContext _db = new ProductContext())
            {
                return _db.Types.ToList();
            }
        }
        public void AddType(Type p)
        {
            using (ProductContext _db = new ProductContext())
            {
                _db.Types.Add(p);
                _db.SaveChanges();
            }
        }
        public void UpdateType(int id, string name)
        {
            using (ProductContext _db = new ProductContext())
            {
                Product p = _db.Products.Find(id);
                p.Name = name;
                _db.Entry(p).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }
        public void DeleteType(int id)
        {
            using (ProductContext _db = new ProductContext())
            {
                Product p = _db.Products.Find(id);
                _db.Entry(p).State = EntityState.Deleted;
                _db.SaveChanges();
            }
        }
        #endregion
        #region Brand
        public List<Brand> RefBrandForView()
        {
            using (ProductContext _db = new ProductContext())
            {
                return _db.Brands.ToList();
            }
        }
        public void AddBrand(Brand p)
        {
            using (ProductContext _db = new ProductContext())
            {
                _db.Brands.Add(p);
                _db.SaveChanges();
            }
        }
        public void UpdateBrand(int id, string name)
        {
            using (ProductContext _db = new ProductContext())
            {
                Product p = _db.Products.Find(id);
                p.Name = name;
                _db.Entry(p).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }
        public void DeleteBrand(int id)
        {
            using (ProductContext _db = new ProductContext())
            {
                Brand p = _db.Brands.Find(id);
                _db.Entry(p).State = EntityState.Deleted;
                _db.SaveChanges();
            }
        }
        #endregion
        #region Customer
        public List<Customer> RefCustomerForView()
        {
            using (ProductContext _db = new ProductContext())
            {
                return _db.Customers.ToList();
            }
        }
        public void AddCustomer(Customer p)
        {
            using (ProductContext _db = new ProductContext())
            {
                _db.Customers.Add(p);
                _db.SaveChanges();
            }
        }
        public void UpdateCustomer(int id, string name,string username, string password, string email, string sdt, string address)
        {
            using (ProductContext _db = new ProductContext())
            {
                Customer p = _db.Customers.Find(id);
                p.Name = name;
                p.UserName = username;
                p.Password = password;
                p.Email = email;
                p.SDT = sdt;
                p.DiaChi = address;
                _db.Entry(p).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }
        public void DeleteCustomer(int id)
        {
            using (ProductContext _db = new ProductContext())
            {
                Customer p = _db.Customers.Find(id);
                _db.Entry(p).State = EntityState.Deleted;
                _db.SaveChanges();
            }
        }
        #endregion
        #region Info
        public List<Info> RefInfoForView()
        {
            using (ProductContext _db = new ProductContext())
            {
                return _db.Infos.ToList();
            }
        }
        public void AddInfo(Info p)
        {
            using (ProductContext _db = new ProductContext())
            {
                _db.Infos.Add(p);
                _db.SaveChanges();
            }
        }
        public void UpdateInfo(int id, string name, string address, string sdt, string email)
        {
            using (ProductContext _db = new ProductContext())
            {
                Info p = _db.Infos.Find(id);
                p.Name = name;
                p.Address = address;
                p.Phone = sdt;
                p.Email = email;
                _db.Entry(p).State = EntityState.Modified;
                _db.SaveChanges();
            }
        }
        public void DeleteInfo(int id)
        {
            using (ProductContext _db = new ProductContext())
            {
                Info p = _db.Infos.Find(id);
                _db.Entry(p).State = EntityState.Deleted;
                _db.SaveChanges();
            }
        }
        #endregion
        #region Promotion
        public List<Promotion> RefPromotionforView()
        {
            using (ProductContext _db = new ProductContext())
            {
                return _db.Promotions.ToList();
            }
        }
        public void AddPromotion(Promotion p, PromotionDetail detail)
        {
            using (ProductContext _db = new ProductContext())
            {
                Promotion obj = _db.Promotions.Where(x => x.ID == p.ID).FirstOrDefault();
                if (obj != null) return;
                _db.Promotions.Add(p);
                _db.SaveChanges();
                detail.PromotionID = _db.Products.Select(i => i.ID).ToList().Max();
                _db.PromotionDetails.Add(detail);
                _db.SaveChanges();
            }
        }
        public void UpdatePromotion(Promotion p, PromotionDetail detail)
        {
            using (ProductContext _db = new ProductContext())
            {
                var obj = _db.Promotions.Where(x => x.ID == p.ID).FirstOrDefault();
                ChangePromotionData(obj, p);
                PromotionDetail promotionDetail = _db.PromotionDetails.Where(x => x.PromotionID == obj.ID).FirstOrDefault();
                ChangePromotioDTData(promotionDetail, detail);
                _db.SaveChanges();
            }
        }
        public void DeletePromotion(int id)
        {
            using (ProductContext _db = new ProductContext())
            {
                Promotion p = _db.Promotions.Find(id);
                _db.Entry(p).State = EntityState.Deleted;
                _db.SaveChanges();
                PromotionDetail d = _db.PromotionDetails.Find(id);
                _db.Entry(d).State = EntityState.Deleted;
                _db.SaveChanges();
            }
        }
        public void ChangePromotionData(Promotion A, Promotion B)
        {
            A.Name = A.Name;
            A.starDate = B.starDate;
            A.endDate = B.endDate;
            A.TrangThai = B.TrangThai;
            A.GiamGia = B.GiamGia;
        }
        public void ChangePromotioDTData(PromotionDetail A, PromotionDetail B)
        {
            A.ProductID = B.ProductID;
        }
        #endregion
        #region Order
        public List<Order> RefOrderForView()
        {
            using (ProductContext _db = new ProductContext())
            {
                return _db.Orders.ToList();
            }
        }
        //them sp
        public void AddOrder(Order p, OrderDetail detail)
        {
            using (ProductContext _db = new ProductContext())
            {
                Product obj = _db.Products.Where(x => x.ID == p.ID).FirstOrDefault();
                if (obj != null) return;
                _db.Orders.Add(p);
                _db.SaveChanges();
                detail.OrderID = _db.Orders.Select(i => i.ID).ToList().Max();
                _db.OrderDetails.Add(detail);
                _db.SaveChanges();
            }
        }
        public void UpdateOrder(Order p, OrderDetail detail)
        {
            using (ProductContext _db = new ProductContext())
            {
                var obj = _db.Orders.Where(x => x.ID == p.ID).FirstOrDefault();
                ChangeOrderData(obj, p);
                OrderDetail orderDetail = _db.OrderDetails.Where(x => x.OrderID == obj.ID).FirstOrDefault();
                ChangeDetailODData(orderDetail, detail);
                _db.SaveChanges();
            }
        }
        public void DeleteOrder(int id)
        {
            using (ProductContext _db = new ProductContext())
            {
                Order p = _db.Orders.Find(id);
                _db.Entry(p).State = EntityState.Deleted;
                _db.SaveChanges();
                OrderDetail d = _db.OrderDetails.Find(id);
                _db.Entry(d).State = EntityState.Deleted;
                _db.SaveChanges();
            }
        }
        public void ChangeOrderData(Order A, Order B)
        {
            A.NgayTao = B.NgayTao;
            A.Note = B.Note;
            A.GiamGia = B.GiamGia;
            A.TrangThai = B.TrangThai;
            A.ChiTietID = B.ChiTietID;
            A.CusID = B.CusID;
        }
        public void ChangeDetailODData(OrderDetail A, OrderDetail B)
        {
            A.ProductID = B.ProductID;
            A.OrderID = B.OrderID;
            A.SoLuong = B.SoLuong;
            A.Gia = B.Gia;
            A.GiamGia = B.GiamGia;
        }
        #endregion
    }
}
