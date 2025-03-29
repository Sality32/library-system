using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Year { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public int BorrowedQuantity { get; set; }

        public ICollection<BorrowedRequest> BorrowedRequests { get; set; } = new List<BorrowedRequest>();
    }
}

