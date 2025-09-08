using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.API.DTOs;
using ShopForHome.API.Services;
using System.Security.Claims;
using ShopForHome.API.Interfaces;
namespace ShopForHome.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly int _userId;

        public ProfileController(IProfileService profileService, IHttpContextAccessor httpContextAccessor)
        {
            _profileService = profileService;
            _userId = int.Parse(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var profile = await _profileService.GetProfileAsync(_userId);
                return Ok(profile);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDTO dto)
        {
            try
            {
                await _profileService.UpdateProfileAsync(_userId, dto);
                return Ok(new { message = "Profile updated successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("address")]
        public async Task<IActionResult> AddAddress(AddressDTO dto)
        {
            try
            {
                var added = await _profileService.AddAddressAsync(_userId, dto);
                return CreatedAtAction(nameof(GetProfile), added);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("address/{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            try
            {
                await _profileService.DeleteAddressAsync(_userId, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
