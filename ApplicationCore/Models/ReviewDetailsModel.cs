using System;
namespace ApplicationCore.Models
{
	public class ReviewDetailsModel
	{
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public int UserId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

