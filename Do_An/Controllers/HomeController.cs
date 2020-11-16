using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Do_An.Models;
using PagedList;

namespace Do_An.Controllers
{
    public class HomeController : MiddleController
    {
        ProductContext _db = new ProductContext();
        [Route("")]
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }
        //tim kiem
        //public ActionResult FindProduct(string key)
        //{
        //    return PartialView()
        //    return View();
        //}

        public void SendCartToMail(string emailUser, string content)
        {

            var fromEmail = new MailAddress("longthinh4126@gmail.com", "3 Bros");
            var toEmail = new MailAddress(emailUser);
            var fromEmailPassword = "123457asd";
            string subject = "Đặt hàng thành công";
            string body = content;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);


        }
        public ActionResult Email(string email)
        {
            string content = "alo";
            SendCartToMail(email, content);
            return RedirectToAction("EmailSucess");
        }
        public ActionResult EmailSucess()
        {
            return View();
        }
        public Product ViewDetailProduct(int id)
        {
            return _db.Products.Find(id);
        }
        public ActionResult ProductDetail(int id)
        {
            var product = ViewDetailProduct(id);
            return View(product);
        }
        #region shop
        public ActionResult Product()
        {
            return View();

        }
        public ActionResult Laptop()
        {
            return View();

        }
        public ActionResult Mouse()
        {
            return View();

        }
        public ActionResult Headphone()
        {
            return View();

        }
        public ActionResult ShowProduct(int? typeid,int page = 0)
        {
            int displayCount = 8;
            if (typeid != null)
            {
                return PartialView("_ListProduct", _db.Products.Where(i => i.TypeID == typeid)
                                                        .ToList()
                                                        .Skip(page * displayCount).Take(displayCount).OrderBy(i => i.Gia).ToList());
            }
            else
            {
                return PartialView("_ListProduct", _db.Products.ToList().Skip(page * displayCount)
                                                                        .Take(displayCount).OrderBy(i => i.Gia).ToList());
            }
        }
        public List<Product> ProductByType(int typeID)
        {
            return _db.Products.Where(x => x.TypeID == typeID).ToList();
        }
        #endregion
        //[OutputCache(Duration = 3600 * 24)]
        public PartialViewResult ShowFooter()
        {
            return PartialView("_footer", _db.Infos.ToList());
        }
        public ActionResult Search(string key)
        {
            return View("_ListProduct", _db.Products.Where(x => x.Name.Contains(key)).ToList());
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Response.Cookies.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoLogin(Customer customer, string ReturnUrl = "")
        {
            var check = _db.Customers.Where(s => s.Email.Equals(customer.Email)
            && s.Password.Equals(customer.Password)).FirstOrDefault();
            if (check == null)
            {
                return View("Index", customer);
            }
            else // dang nhap thanh cong
            {
                var test = _db.Customers.FirstOrDefault(s => s.Email == customer.Email);
             
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else if (test.Quyen.Equals("Admin"))
                {
                    SharedSession["Quyen"] = "Admin";
                    Session["Email"] = test.Email.ToString();
                    Session["Name"] = test.Name.ToString();
                    return RedirectToAction("Customer", "Admin");
                }
                else
                {
                    SharedSession["Quyen"] = "Customer";
                    Session["Email"] = customer.Email.ToString();
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Customers.FirstOrDefault(s => s.Email == customer.Email);
                if (check == null)
                {
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Customers.Add(customer);
                    customer.Quyen = "Customner";
                    _db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Tài khoản email đã có người đăng ký ! Xin hãy nhập email khác.";
                    return View();
                }
            }
            return View(customer);
        }
    }
}