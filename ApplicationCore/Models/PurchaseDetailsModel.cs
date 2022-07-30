using System;
namespace ApplicationCore.Models
{
    public class PurchaseDetailsModel
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid PurchaseNumber { get; set; }
        public DateTime PurchaseDatetTime { get; set; }
    }
}

