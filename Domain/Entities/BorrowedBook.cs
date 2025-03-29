using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BorrowedBook
    {
        public Guid Id { get; set; }
        public Guid BorrowedRequestId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        [ForeignKey("BorrowedRequestId")]
        public BorrowedRequest BorrowedRequest { get; set; }
    }
}

