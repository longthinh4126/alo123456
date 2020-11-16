using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Do_An.Models;

namespace Do_An.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        ProductContext _db = new ProductContext();
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            var productInCart = Session["Cart"];
            if(cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddToCart(int id, int quantity)
        {
            var pro = _db.Products.Find(id);
            if(pro != null)
            {
                GetCart().Add(pro, quantity);
            }
            return RedirectToAction("ShowToCart");
        }
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] == null)
            {
                return RedirectToAction("ErrorCart", "Cart");

            }
            else
            {
                Cart cart = Session["Cart"] as Cart;
                return View(cart);
            }
        }
        public ActionResult Update_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["ID"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.Update_Quantity_Cart(id_pro, quantity);
            return RedirectToAction("ShowToCart", "Cart");
        }
        public ActionResult DeleteCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowToCart", "Cart");
        }
        public ActionResult ErrorCart()
        {
            return View();
        }
        //checkout
        public ActionResult Checkout(FormCollection form)
        {
            if(form != null)
            {
                Cart cart = Session["Cart"] as Cart;
                Order _order = new Order();
                _order.NgayTao = DateTime.Now;
                _order.TenKH = form["Name"];
                _order.DiaChiKH = form["Address"];
                _order.SDTKH = form["SDT"];
                _order.Note = form["Note"];
                foreach (var item in cart.Items)
                {
                    OrderDetail _orderDetail = new OrderDetail();
                    _orderDetail.OrderID = _order.ID;
                    _orderDetail.ProductID = item._shopProduct.ID;
                    _orderDetail.Gia = item._shopProduct.Gia;
                    _orderDetail.SoLuong = item._shopQuantity;
                    _orderDetail.TongCong = _orderDetail.SoLuong * _orderDetail.Gia;
                    _db.OrderDetails.Add(_orderDetail);
                    _order.TongGia += _orderDetail.TongCong;
                }
                _db.Orders.Add(_order);
                _db.SaveChanges();
                cart.ClearCart();
                return View();
            }
            else
            {
                return Content("Hãy nhập thông tin!");
            }
        }
        public ActionResult Camon()
        {
            return View();
        }
    }
}