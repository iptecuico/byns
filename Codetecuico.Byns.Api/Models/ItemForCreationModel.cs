using System;
using System.ComponentModel.DataAnnotations;

namespace Codetecuico.Byns.Api.Models
{
    public class ItemForCreationModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Condition { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public double Price { get; set; }

        public string Status { get; set; }
        public int StockCount { get; set; }
        public int StarCount { get; set; }
        public string Image { get; set; }
        public DateTime DatePosted { get; set; }
        public bool IsSold { get; set; }
        public string Remarks { get; set; }
    }
}
