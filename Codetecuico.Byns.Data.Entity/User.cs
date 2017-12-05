using System;

namespace Codetecuico.Byns.Data.Entity
{
    public class User : BaseModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string PersonalWebsite { get; set; }
        public string ExternalId { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}