using ShopForHome.API.DTOs;

namespace ShopForHome.API.Interfaces
{
    public interface IAdminUserService
    {
        Task<List<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(int id);
        Task UpdateUserAsync(UserDTO userDto);
        Task DeactivateUserAsync(int id);
    }
}
