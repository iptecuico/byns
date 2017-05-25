using System;

namespace Codetecuico.Byns.Common.Domain
{
    public class Member : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateJoined { get; set; }
    }
}