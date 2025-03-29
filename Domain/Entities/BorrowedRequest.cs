using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class BorrowedRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status {  get; set; }
        public BorrowedBook BorrowedBook { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }
        

    }
}

