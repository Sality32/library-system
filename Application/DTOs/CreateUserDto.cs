namespace Application.DTOs
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActived { get; set; }
        public string Role { get; set; }
    }
}
