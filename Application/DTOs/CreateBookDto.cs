namespace Application.DTOs
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
