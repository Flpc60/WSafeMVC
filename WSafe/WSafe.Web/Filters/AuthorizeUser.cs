using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSafe.Domain.Data.Entities;
using WSafe.Web.Models;

namespace WSafe.Web.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private User _usuario;
        private readonly EmpresaContext _empresaContext = new EmpresaContext();
        private int _operationID;
        private int _roleID;
        public AuthorizeUser(int roleID, int operationID)
        {
            _roleID = roleID;
            _operationID = operationID;
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var operation = "";

            try
            {
                _usuario = (User)HttpContext.Current.Session["User"];
                var result = _empresaContext.RoleOperations.Where(ro => ro.RolID == 1 && ro.OperationID == 1).Count();
                if (result < 1)
                {
                    operation = _empresaContext.Operations.FirstOrDefault(o => o.ID == _operationID).Name;
                    filterContext.Result = new RedirectResult("~/Error/UnAuthorizedOperation=" + operation);
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnAuthorizedOperation=" + operation);
            }
        }
    }
}