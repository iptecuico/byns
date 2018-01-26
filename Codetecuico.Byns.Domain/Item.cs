using System;

namespace Codetecuico.Byns.Domain
{
    public class Item: BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Condition { get; set; }
        public string Currency { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public int StockCount { get; set; }
        public int StarCount { get; set; }
        public string Image{ get; set; }
        public DateTime DatePosted { get; set; }
        public bool IsSold { get; set; }
        public string Remarks { get; set; }
        public int UserId { get; set; }
        public Guid OrganizationId { get; set; }


        public virtual User User { get; set; }
        public virtual Organization Organization { get; set; }
    }
}