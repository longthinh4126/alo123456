using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Do_An.Models;
using System.IO;

namespace Do_An.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
       
        // GET: Admin
        ProductContext __db = new ProductContext();
        public ActionResult Index()
        {
            return View();
        }
        #region Product
        //.......Index.........//
        public ActionResult Product()
        {
            var products = __db.Products.Include(p => p.Brand).Include(p => p.Type);
            return View(products.ToList());
        }
        //.......Thêm sp........//
        public ActionResult ThemSanPham()
        {
            Product product = new Product();
            ViewBag.IDBrand = new SelectList(__db.Brands, "ID", "Name");
            ViewBag.IDCate = new SelectList(__db.Types, "ID", "Name");
            return View(product);
        }
        [HttpPost]
        public ActionResult ThemSanPham(Product pro, HttpPostedFile img )
        {
           
                var path = "";
            var filename = "";
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/Images/Product/"), filename);

                    img.SaveAs(path);
                    pro.Image = filename;
                }
                QuanLy ql = new QuanLy();
                ql.AddProduct(pro);
            
                return RedirectToAction("Product");
            }
            ViewBag.IDbrand = new SelectList(__db.Brands, "ID", "Name", pro.ID);
            ViewBag.IDType = new SelectList(__db.Types, "ID", "Name", pro.ID);
            return View(pro);
        }
        //.........Xoá sp.........//
        public ActionResult XoaSanPham(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = __db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        [HttpPost]
        public ActionResult XoaSanPham(int id)
        {
            QuanLy ql = new QuanLy();
            ql.DeleteProduct(id);
            return RedirectToAction("Product");
        }
        //..........Sua sp...........//
        public ActionResult SuaSanPham(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = __db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBrand = new SelectList(__db.Brands, "ID", "Name", pro.ID);
            ViewBag.IDType = new SelectList(__db.Types, "ID", "Name", pro.ID);
            return View(pro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaSanPham(Product pro, ProductDetail detail)
        {
            if (ModelState.IsValid)
            {
                if (pro.ImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pro.ImageUpload.FileName);
                    string extension = Path.GetExtension(pro.ImageUpload.FileName);
                    filename = filename + extension;
                    pro.Image = "~/Content/Images/Product/" + filename;
                    pro.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/Images/Product/"), filename));
                }
                QuanLy ql = new QuanLy();
                ql.UpdateProduct(pro);
                __db.Entry(pro).State = EntityState.Modified;
                __db.SaveChanges();
                ViewBag.IDBrand = new SelectList(__db.Brands, "ID", "Name", pro.ID);
                ViewBag.IDType = new SelectList(__db.Types, "ID", "Name", pro.ID);
                return RedirectToAction("Product");
            }
            return View(pro);
        }
        #endregion
        #region Brand
        //.......Index.........//
        public ActionResult Brand()
        {
            var brands = __db.Brands;
            return View(brands.ToList());
        }
        //.......Thêm brand........//
        public ActionResult ThemBrand()
        {
            Brand brand = new Brand();
            ViewBag.IDBrand = new SelectList(__db.Brands, "ID", "Name");
            return View(brand);
        }
        [HttpPost]
        public ActionResult ThemBrand(Brand p)
        {
            if (ModelState.IsValid)
            {
                QuanLy ql = new QuanLy();
                ql.AddBrand(p);
                return RedirectToAction("Brand");
            }
            ViewBag.IDbrand = new SelectList(__db.Brands, "ID", "Name", p.ID);
            return View(p);
        }
        //.........Xoá brand.........//
        public ActionResult XoaBrand(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand pro = __db.Brands.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult XoaBrand(int id)
        {
            QuanLy ql = new QuanLy();
            ql.DeleteBrand(id);
            return RedirectToAction("Brand");
        }
        //..........Sua brand...........//
        public ActionResult SuaBrand(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = __db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDBrand = new SelectList(__db.Brands, "ID", "Name", pro.ID);
            ViewBag.IDType = new SelectList(__db.Types, "ID", "Name", pro.ID);
            return View(pro);
        }
        [HttpPost]
        public ActionResult SuaBrand(Brand p, string name)
        {
            if (ModelState.IsValid)
            {
                QuanLy ql = new QuanLy();
                ql.UpdateBrand(p.ID, name);
                __db.Entry(p).State = EntityState.Modified;
                __db.SaveChanges();
                ViewBag.IDBrand = new SelectList(__db.Brands, "ID", "Name", p.ID);
                return RedirectToAction("Brand");
            }
            return View(p);
        }
        #endregion
        #region Customer
        //.......Index.........//
        public ActionResult Customer()
        {
            var customers = __db.Customers;
            return View(customers.ToList());
        }
        //.......Thêm Customer........//
        public ActionResult ThemCustomer()
        {
            Customer customer = new Customer();
            ViewBag.IDCustomer = new SelectList(__db.Customers, "ID", "Name");
            return View(customer);
        }
        [HttpPost]
        public ActionResult ThemCustomer(Customer p)
        {
            if (ModelState.IsValid)
            {
                QuanLy ql = new QuanLy();
                ql.AddCustomer(p);
                return RedirectToAction("Customer");
            }
            ViewBag.IDCustomer = new SelectList(__db.Customers, "ID", "Name", p.ID);
            return View(p);
        }
        //.........Xoá Customer.........//
        public ActionResult XoaCustomer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer pro = __db.Customers.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        [HttpPost]
        public ActionResult XoaCustomer(int id)
        {
            QuanLy ql = new QuanLy();
            ql.DeleteCustomer(id);
            return RedirectToAction("Customer");
        }
        //..........Sua Customer...........//
        public ActionResult SuaCustomer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer pro = __db.Customers.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDCustomer = new SelectList(__db.Customers, "ID", "Name", pro.ID);
            return View(pro);
        }
        [HttpPost]
        public ActionResult SuaCustomer(Customer p,int id, string name, string user, string password, string email, string sdt, string diachi)
        {
            if (ModelState.IsValid)
            {
                QuanLy ql = new QuanLy();
                ql.UpdateCustomer(id,name,user,password,email,sdt,diachi);
                __db.Entry(p).State = EntityState.Modified;
                __db.SaveChanges();
                ViewBag.IDCustomer = new SelectList(__db.Customers, "ID", "Name", p.ID);
                return RedirectToAction("Customer");
            }
            return View(p);
        }
        #endregion
        #region Type
        //.......Index.........//
        public ActionResult Type()
        {
            var types = __db.Types;
            return View(types.ToList());
        }
        //.......Thêm Type........//
        public ActionResult ThemType()
        {
            Do_An.Models.Type types = new Models.Type();
            ViewBag.IDType = new SelectList(__db.Types, "ID", "Name");
            return View(types);
        }
        [HttpPost]
        public ActionResult ThemType(Models.Type p)
        {
            if (ModelState.IsValid)
            {
                QuanLy ql = new QuanLy();
                ql.AddType(p);
                return RedirectToAction("Type");
            }
            ViewBag.IDType = new SelectList(__db.Types, "ID", "Name", p.ID);
            return View(p);
        }
        //.........Xoá Type.........//
        public ActionResult XoaType(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type pro = __db.Types.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        [HttpPost]
        public ActionResult XoaType(int id)
        {
            QuanLy ql = new QuanLy();
            ql.DeleteType(id);
            return RedirectToAction("Type");
        }
        //..........Sua Type...........//
        public ActionResult SuaType(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type pro = __db.Types.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDType = new SelectList(__db.Types, "ID", "Name", pro.ID);
            return View(pro);
        }
        [HttpPost]
        public ActionResult SuaType(Customer p, int id, string name)
        {
            if (ModelState.IsValid)
            {
                QuanLy ql = new QuanLy();
                ql.UpdateType(id, name);
                __db.Entry(p).State = EntityState.Modified;
                __db.SaveChanges();
                ViewBag.IDType = new SelectList(__db.Types, "ID", "Name", p.ID);
                return RedirectToAction("Type");
            }
            return View(p);
        }
        #endregion
        #region Promotion
        //.......Index.........//
        public ActionResult Promotion()
        {
            var promotions = __db.Promotions.Include(p => p.TrangThai);
            return View(promotions.ToList());
        }
        //.......Thêm Promotion........//
        public ActionResult ThemPromotion()
        {
            Promotion promotion = new Promotion();
            ViewBag.IDPromo = new SelectList(__db.Promotions, "ID", "Name");
            return View(promotion);
        }
        [HttpPost]
        public ActionResult ThemPromotion(Promotion pro, PromotionDetail detail)
        {
            if (ModelState.IsValid)
            {
                QuanLy ql = new QuanLy();
                ql.AddPromotion(pro, detail);
                return RedirectToAction("Promotion");
            }
            ViewBag.IDPromo = new SelectList(__db.Promotions, "ID", "Name", pro.ID);
            return View(pro);
        }
        //.........Xoá Promotion.........//
        public ActionResult XoaPromotion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion pro = __db.Promotions.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        [HttpPost]
        public ActionResult XoaPromotion(int id)
        {
            QuanLy ql = new QuanLy();
            ql.DeletePromotion(id);
            return RedirectToAction("Promotion");
        }
        //..........Sua Promotion...........//
        public ActionResult SuaPromotion(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = __db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDPromotion = new SelectList(__db.Brands, "ID", "Name", pro.ID);
            return View(pro);
        }
        [HttpPost]
        public ActionResult SuaPromotion(Promotion pro, PromotionDetail detail)
        {
            if (ModelState.IsValid)
            {
                QuanLy ql = new QuanLy();
                ql.UpdatePromotion(pro, detail);
                __db.Entry(pro).State = EntityState.Modified;
                __db.SaveChanges();
                __db.Entry(detail).State = EntityState.Modified;
                __db.SaveChanges();
                ViewBag.IDPromotion = new SelectList(__db.Brands, "ID", "Name", pro.ID);
                return RedirectToAction("Promotion");
            }
            return View(pro);
        }
        #endregion
        #region Order
        //.......Index.........//
        public ActionResult Order()
        {
            var orders = __db.Orders;
            return View(orders.ToList());
        }
        //.......Thêm Order........//
        public ActionResult ThemOrder()
        {
            Order order = new Order();
            ViewBag.IDOrder = new SelectList(__db.Orders, "ID", "Name");
            return View(order);
        }
        [HttpPost]
        public ActionResult ThemOrder(Order pro, OrderDetail detail)
        {
            if (ModelState.IsValid)
            {
                QuanLy ql = new QuanLy();
                ql.AddOrder(pro, detail);
                return RedirectToAction("Order");
            }
            ViewBag.IDOrder = new SelectList(__db.Orders, "ID", "Name", pro.ID);
            return View(pro);
        }
        //.........Xoá Order.........//
        public ActionResult XoaOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order pro = __db.Orders.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        [HttpPost]
        public ActionResult XoaOrder(int id)
        {
            QuanLy ql = new QuanLy();
            ql.DeleteOrder(id);
            return RedirectToAction("Order");
        }
        //..........Sua Order...........//
        public ActionResult SuaOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order pro = __db.Orders.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDOder = new SelectList(__db.Orders, "ID", "Name", pro.ID);
            return View(pro);
        }
        [HttpPost]
        public ActionResult SuaOrder(Order pro, OrderDetail detail)
        {
            if (ModelState.IsValid)
            {
                QuanLy ql = new QuanLy();
                ql.UpdateOrder(pro, detail);
                __db.Entry(pro).State = EntityState.Modified;
                __db.SaveChanges();
                __db.Entry(detail).State = EntityState.Modified;
                __db.SaveChanges();
                ViewBag.IDOrder = new SelectList(__db.Orders, "ID", "Name", pro.ID);
                return RedirectToAction("Order");
            }
            return View(pro);
        }
        #endregion
        #region Info
        //.......Index.........//
        public ActionResult Info()
        {
            var infos = __db.Infos;
            return View(infos.ToList());
        }
        //.......Thêm Type........//
        public ActionResult ThemInfo()
        {
            Do_An.Models.Info info = new Models.Info();
            ViewBag.IDInfo = new SelectList(__db.Infos, "ID", "Name");
            return View(info);
        }
        [HttpPost]
        public ActionResult ThemInfo(Models.Info p)
        {
            if (ModelState.IsValid)
            {
                QuanLy ql = new QuanLy();
                ql.AddInfo(p);
                return RedirectToAction("Info");
            }
            ViewBag.IDInfo = new SelectList(__db.Infos, "ID", "Name", p.ID);
            return View(p);
        }
        //.........Xoá Type.........//
        public ActionResult XoaInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Info pro = __db.Infos.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            return View(pro);
        }
        [HttpPost]
        public ActionResult XoaInfo(int id)
        {
            QuanLy ql = new QuanLy();
            ql.DeleteType(id);
            return RedirectToAction("Type");
        }
        //..........Sua Type...........//
        public ActionResult SuaInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type pro = __db.Types.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDInfo = new SelectList(__db.Infos, "ID", "Name", pro.ID);
            return View(pro);
        }
        [HttpPost]
        public ActionResult SuaInfo(Info p, int id, string name, string diachi, string phone, string email)
        {
            if (ModelState.IsValid)
            {
                QuanLy ql = new QuanLy();
                ql.UpdateInfo(id, name, diachi, phone, email);
                __db.Entry(p).State = EntityState.Modified;
                __db.SaveChanges();
                ViewBag.IDInfo = new SelectList(__db.Infos, "ID", "Name", p.ID);
                return RedirectToAction("Info");
            }
            return View(p);
        }
        #endregion
    }
}