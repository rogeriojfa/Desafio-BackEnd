
using MotoRent.Domain.Enums;

namespace MotoRent.DTOs
{
    public class CreateUserDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
