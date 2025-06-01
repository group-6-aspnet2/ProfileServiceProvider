using Business.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController(IProfileService profileService) : ControllerBase
    {
        private readonly IProfileService _profileService = profileService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var profiles = await _profileService.GetAllAsync();
            if(profiles == null || !profiles.Any())
                return NotFound("No profiles found.");

            return Ok(profiles);
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest("User ID cannot be null or empty.");

            var profile = await _profileService.GetAsync(userId);
            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> Create(string userId, [FromBody] CreateProfileModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid profile data.");

            var result = await _profileService.CreateAsync(model, userId);

            return Ok(result);
        }

    }
}