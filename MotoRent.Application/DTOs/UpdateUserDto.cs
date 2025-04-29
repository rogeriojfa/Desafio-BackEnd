
using MotoRent.Domain.Enums;

namespace MotoRent.DTOs
{
    public class UpdateUserDto
    {
        public required string Name { get; set; }
        public UserRole Role { get; set; }
    }
}
