namespace Codetecuico.Byns.Domain
{
    public class UserRoleAccess
    {
        public int Id { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int UserRoleId { get; set; }
    }
}
