using System;
using System.Collections.Generic;

namespace Codetecuico.Byns.Domain
{
    public class Organization : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
