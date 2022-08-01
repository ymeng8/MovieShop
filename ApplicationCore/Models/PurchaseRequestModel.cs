using System;
namespace ApplicationCore.Models
{
    public class PurchaseRequestModel
    {
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public Guid PurchaseNumber => Guid.NewGuid();
        public DateTime PurchaseDateTime => DateTime.UtcNow;
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

