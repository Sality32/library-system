using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> AddAsync(CreateUserDto createUserDto) 
        {
            var user = new User()
            {
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                IsActived = createUserDto.IsActived,
                Role = createUserDto.Role,
            };
            user.SetPassword(createUserDto.Password);

            await userRepository.AddAsync(user);
            return user;
        }

        public async Task<User?> GetByEmail(string email)
        {
            var user = await userRepository.GetByEmail(email);
            return user;
        }
    }
}