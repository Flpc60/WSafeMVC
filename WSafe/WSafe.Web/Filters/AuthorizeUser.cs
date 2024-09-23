using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSafe.Domain.Data;
using WSafe.Domain.Data.Entities;

namespace WSafe.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private User _usuario;
        private readonly EmpresaContext _empresaContext = new EmpresaContext();
        private int _operation;
        private int _component;
        private int _roleID;
        public AuthorizeUser(int operation, int component)
        {
            _operation = operation;
            _component = component;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var textOperation = "";

            try
            {
                _roleID = (int)HttpContext.Current.Session["roleID"]; // castear _roleID (int)
                var result = _empresaContext.RoleOperations.Where(ro => ro.RoleID == _roleID && ro.Operation == _operation && ro.Component == _component).Count();
                if (result < 1)
                {
                    textOperation = "Usted no tiene autorización para trabajar en esta página";
                    filterContext.Result = new RedirectResult("~/Home/Error/UnAuthorizedOperation=" + textOperation);
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Home/Error/UnAuthorizedOperation=" + ex.Message);
            }
        }
    }
}