using System;
using System.Collections.Generic;

namespace Codetecuico.Byns.Domain
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
        public Guid OrganizationId { get; set; }
        public int UserRoleId { get; set; }

        public virtual Organization Organization { get; set; }
        public UserRole UserRole { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}