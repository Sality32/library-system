using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActived { get; set; }
        public string Role { get; set; }

        public ICollection<BorrowedRequest> BorrowedRequests { get; set; } = new List<BorrowedRequest>();

        private static readonly PasswordHasher<User> _passwordHasher = new();

        public void SetPassword(string password)
        {
            Password = _passwordHasher.HashPassword(this, password);
        }

        public bool VerifyPassword(string password)
        {
            return _passwordHasher.VerifyHashedPassword(this, Password, password) == PasswordVerificationResult.Success;
        }

        public static List<User> PredefineUsers()
        {
            var users = new List<User>()
                {
                    new User { Id = Guid.NewGuid(), Email = "admin@example.com", Password = "admin1234", IsActived = true, Name = "Admin", Role = "Admin" },
                    new User { Id = Guid.NewGuid(), Email = "officer@example.com", Password = "officer12345", IsActived = true, Name = "Officer", Role = "Officer" },
                    new User { Id = Guid.NewGuid(), Email = "user@example.com", Password = "user1234", IsActived = true, Name = "User", Role = "User" }
                } ;

            foreach (var user in users) 
            {
                user.SetPassword(user.Password);
            }
            return users;
        }
    }
}

