using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace Do_An.Controllers
{
    public class MiddleController : Controller
    {
        // GET: Middle
        public HttpSessionState SharedSession
        {
            get
            {
                return System.Web.HttpContext.Current.Session;
            }
        }
        public class SessionAuthorizeAttribute : AuthorizeAttribute
        {
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                var middle = new MiddleController();
                return (string)middle.SharedSession["Role"] == "Admin";
            }
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                filterContext.Result = new RedirectToRouteResult(
                                  new RouteValueDictionary
                                  (new { action = "Login", controller = "Home" }));
            }
        }
    }
}
