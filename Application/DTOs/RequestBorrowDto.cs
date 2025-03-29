using System;

namespace Application.DTOs
{
    public class CreateBorrowedRequestDto
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}
