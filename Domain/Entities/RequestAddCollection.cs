using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class RequestAddCollection
    {
        public Guid Id { get; set; }
        public string DetailBooksKey { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}

