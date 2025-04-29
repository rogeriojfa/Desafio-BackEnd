
using System;
using MassTransit.Context;
using MotoRent.Domain.Enums;

namespace MotoRent.DTOs
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public UserRole Role { get; set; }
        public required string Token { get; set; }
    }
}
