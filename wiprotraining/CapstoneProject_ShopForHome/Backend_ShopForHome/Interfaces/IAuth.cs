using ShopForHome.API.DTOs;

namespace ShopForHome.API.Interfaces
{
    public interface IAuthService
    {
        Task<UserDTO> RegisterAsync(CreateUserDTO createUserDto);
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    }
}
