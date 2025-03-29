using System;

namespace Application.DTOs
{
    public class ApprovalBorrowedRequestDto
    {
        public Guid BorrowedRequestId { get; set; }
        public string Status { get; set; }
    }
}
