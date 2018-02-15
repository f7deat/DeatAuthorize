using DeatAuthorize.Common;
using DeatAuthorize.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DeatAuthorize.Security
{
    public class DeatAuthorizeAttribute: AuthorizeAttribute
    {
        //1: Member
        //2: Moderator
        //3: Admin
        //4: ...

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            AccountModel db = new AccountModel();
            var session = (User)HttpContext.Current.Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", area = "Admin" }));
            }
            else
            {
                var hasRole = session.Role;
                if (hasRole < Order)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "NotAuthorize" }));
                }
            }
        }
    }
}