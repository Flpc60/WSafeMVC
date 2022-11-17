namespace WSafe.Web.Models
{
    public class AuthorizationVM
    {
        public int ID { get; set; }
        public int RoleID { get; set; }
        public string Role { get; set; }
        public int ComponentID { get; set; }
        public string Component { get; set; }
        public int OperationID { get; set; }
        public string Operation { get; set; }

    }
}