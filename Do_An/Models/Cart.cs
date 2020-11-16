using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Do_An.Models
{
    public class CartItem
    {
        public Product _shopProduct { get; set; }
        public int _shopQuantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }
        public void Add(Product pro, int quantity = 1)
        {
            var giohang = items.FirstOrDefault(i => i._shopProduct.ID == pro.ID);
            if(giohang == null)
            {
                items.Add(new CartItem
                {
                    _shopProduct = pro,
                    _shopQuantity = quantity
                });
            }
            else if(quantity >0)
            {
                giohang._shopQuantity += quantity;
            }
        }
        public void Update_Quantity_Cart(int id, int quantity)
        {
            var item = items.Find(s => s._shopProduct.ID == id);
            if(item != null && quantity >0)
            {
                item._shopQuantity = quantity;
            }
        }
        public void Remove_CartItem(int id)
        {
            items.RemoveAll(s => s._shopProduct.ID == id);
        }
        public int Total_Quantity_Cart()
        {
            return items.Sum(s => s._shopQuantity);
        }
        public void ClearCart()
        {
            items.Clear();//xoa gio hang de thuc hien order
        }
    }
}