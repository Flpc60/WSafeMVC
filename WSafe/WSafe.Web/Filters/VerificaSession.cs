using System;
using System.Web;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Web.Controllers;

namespace WSafe.Web.Filters
{
    public class VerificaSession : System.Web.Mvc.ActionFilterAttribute
    {
        private User usuario;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                usuario = (User)HttpContext.Current.Session["User"];
                if(usuario == null)
                {
                    if(filterContext.Controller is AccountsController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("~/Accounts/Login");

                    }
                }

            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Accounts/Logout");
            }
        }

    }
}