using System;
namespace ApplicationCore.Entities
{
    public class Purchase
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid PurchaseNumber { get; set; }
        public DateTime PurchaseDatetTime { get; set; }

        public User Customer { get; set; }
        public Movie Movie { get; set; }
    }
}

