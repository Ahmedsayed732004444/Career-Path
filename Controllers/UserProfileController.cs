using Career_Path.Contracts.UserProfile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Career_Path.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController(IUserProfileService userProfileService) : ControllerBase
    {
        private readonly IUserProfileService _userProfileService = userProfileService;

        [HttpGet]
        public async Task<IActionResult> GetUserProfile()
        {
            return Ok(await _userProfileService.GetAsync(User.GetUserId()!));
        }
        [HttpPost]
        public async Task<IActionResult> AddUserProfile([FromForm] UserProfileRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileService.AddProfileAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? Created() : result.ToProblem();
        }

        [HttpPut("basic-data")]
        public async Task<IActionResult> UpdateBasicDaya([FromBody] UserProfileBasicRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileService.UpdateBasicDataAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpPut("soft-skills")]
        public async Task<IActionResult> UpdateSoftSkills([FromBody] UpdateUserProfileSoftSkillsRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileService.UpdateSoftSkillsAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpPut("hard-skills")]
        public async Task<IActionResult> UpdateHardSkills([FromBody] UpdateUserProfileHardSkillsRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileService.UpdateHardSkillsAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpPut("cv")]
        public async Task<IActionResult> UpdateCv([FromForm] UpdateUserProfileCvRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileService.UpdateCvAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpPut("picture")]
        public async Task<IActionResult> UpdatePicture([FromForm] UpdateUserProfilePictureRequest request, CancellationToken cancellationToken)
        {
            var result = await _userProfileService.UpdatePictureAsync(User.GetUserId()!, request, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpDelete("cv")]
        public async Task<IActionResult> DeleteCv(CancellationToken cancellationToken)
        {
            var result = await _userProfileService.DeleteCvAsync(User.GetUserId()!, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [HttpDelete("picture")]
        public async Task<IActionResult> DeletePicture(CancellationToken cancellationToken)
        {
            var result = await _userProfileService.DeletePictureAsync(User.GetUserId()!, cancellationToken);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }

}
