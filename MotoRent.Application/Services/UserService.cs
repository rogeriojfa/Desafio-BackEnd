
using MotoRent.Application.Interfaces.Service;
using MotoRent.Application.Services;
using MotoRent.Domain.Entities;
using MotoRent.Domain.Interfaces.Repositories;
using MotoRent.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoRent.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };

            await _userRepository.AddAsync(user);

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Token = "KJFKALGJALlkflkdgldçdkgçldagkç123456"
            };
        }


        public async Task<UserResponseDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || !VerifyPassword(user, password))
                throw new UnauthorizedAccessException("Invalid credentials.");

            var token = _tokenService.GenerateToken(user.Id, user.Email, user.Role.ToString());

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,    // se você tem isso no User
                Token = token
            };
        }


        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role,
                Token = "KJFKALGJALlkflkdgldçdkgçldagkç123456"
            });
        }

        public async Task<UserResponseDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Token = "KJFKALGJALlkflkdgldçdkgçldagkç123456"
            };
        }

        public async Task UpdateAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("Usuário não encontrado.");

            user.Name = dto.Name;
            user.Role = dto.Role;

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

        private bool VerifyPassword(User user, string password)
        {
            // Se sua senha estiver armazenada como plain-text (não recomendado em produção)
            return user.PasswordHash == password;
        }
    }
}
