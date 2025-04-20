using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Application.DTOs
{
    public class ApiExternalResponseDto
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("next")]
        public string? Next { get; set; }
        [JsonProperty("previous")]
        public string? Previous { get; set; }
        [JsonProperty("results")]
        public List<ResultObject> Results { get; set; }

        public class ResultObject
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("title")]
            public string? Title { get; set; }

            [JsonProperty("authors")]
            public List<Author> Authors { get; set; }

            [JsonProperty("summaries")]
            public List<string> Descriptions { get; set; }
            [JsonProperty("subjects")]
            public List<string> Categories { get; set; }
            [JsonProperty("formats")]
            public Format Format { get; set; }

        }


        public class Author
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }

        public class Format
        {
            [JsonProperty("image/jpeg")]
            public string ImageUrl { get; set; }
        }
    }
}
