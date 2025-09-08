using ShopForHome.API.DTOs;

namespace ShopForHome.API.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDTO> GetProfileAsync(int userId);
        Task UpdateProfileAsync(int userId, UpdateProfileDTO dto);
        Task<AddressDTO> AddAddressAsync(int userId, AddressDTO dto);
        Task DeleteAddressAsync(int userId, int addressId);
    }
}
