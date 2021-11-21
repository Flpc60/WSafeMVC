using System.Data.Entity;

namespace WSafe.Web.Models
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext() : base("DefaultConnection")
        {

        }
    }
}