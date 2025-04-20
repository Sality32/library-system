using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class RequestAddCollectionDto
    {

        public Guid Id { get; set; }
        public string DetailBooksKey { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public List<CreateBookDto> BookObjects { get; set; }
    }
}
