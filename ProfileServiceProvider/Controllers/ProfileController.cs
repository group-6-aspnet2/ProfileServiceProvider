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

    [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var profile = await _profileService.GetAsync(userId);
            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> Create(int userId, [FromBody] CreateProfileModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid profile data.");

            var result = await _profileService.CreateAsync(model, userId);

            return Ok(result);
        }
    }
}