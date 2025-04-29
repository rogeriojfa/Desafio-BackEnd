using MotoRent.Application.DTOs;
using MotoRent.DTOs;

namespace MotoRent.Application.Interfaces.Service
{


    public interface IUserService
    {
        Task<UserResponseDto> CreateAsync(CreateUserDto createUserDto);
        Task<UserResponseDto> LoginAsync(string email, string password);
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto> GetByIdAsync(Guid id);
        Task UpdateAsync(Guid id, UpdateUserDto dto);
        Task DeleteAsync(Guid id);

    }
}
