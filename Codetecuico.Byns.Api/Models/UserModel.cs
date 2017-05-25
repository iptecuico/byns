using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Codetecuico.Byns.Api.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
        public string MobileNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Location { get; set; }
        public string PersonalWebsite { get; set; }
        [JsonIgnore]
        public string ExternalId { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}