using System;

namespace Codetecuico.Byns.Data.Entity
{
    public class Member : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateJoined { get; set; }
    }
}