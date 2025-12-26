using Career_Path.Contracts.UserProfile;
using Career_Path.Entities;
using Microsoft.AspNetCore.Identity;

namespace Career_Path.Services
{
    public class UserProfileService(
        ApplicationDbContext context, ILogger<UserProfileService> logger,
        IWebHostEnvironment env, IHttpContextAccessor accessor): IUserProfileService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<UserProfileService> _logger = logger;
        private readonly IHttpContextAccessor _accessor = accessor;
        private readonly IWebHostEnvironment _env = env;

        public async Task<UserProfileResponse> GetAsync(string applicationUserId)
        {
            var user = await _context.UserProfiles.
                   Include(up => up.ApplicationUser).Include(up => up.SoftSkills).Include(up => up.HardSkills).FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
            return user.Adapt<UserProfileResponse>();
        }

        public async Task<Result> AddProfileAsync(string applicationUserId, UserProfileRequest request, CancellationToken cancellationToken = default)
        {
                var user = await _context.UserProfiles.Include(up => up.ApplicationUser).
                    Include(up => up.SoftSkills).Include(up => up.HardSkills).
                    FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
                if (user is not null)
                    return Result.Failure(UserErrors.DuplicateProfile);
                var userProfile = request.Adapt<UserProfile>();
                userProfile.ApplicationUserId = applicationUserId;
                if (request.ProfilePicture is not null)
                    userProfile.ProfilePictureUrl = await FileHelper.UploadeFileAsync(request.ProfilePicture, "Images", _env, _accessor);
                if (request.CvFile is not null)
                    userProfile.CvFileUrl = await FileHelper.UploadeFileAsync(request.CvFile, "CvS", _env, _accessor);
                _context.UserProfiles.Add(userProfile);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success(); 
        }

        public async Task<Result> UpdateBasicDataAsync(string applicationUserId, UserProfileBasicRequest request, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles.Include(up => up.ApplicationUser).
                FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);
            request.Adapt(userProfile);
            userProfile.ApplicationUser.FirstName = request.FirstName;
            userProfile.ApplicationUser.LastName = request.LastName;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> UpdateCvAsync(string applicationUserId,UpdateUserProfileCvRequest request,CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId, cancellationToken);

            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);

            if (!string.IsNullOrEmpty(userProfile.CvFileUrl))
            {
                FileHelper.DeleteFile(userProfile.CvFileUrl, "CvS", _env);
            }
            userProfile.CvFileUrl = await FileHelper.UploadeFileAsync(request.CvFile, "CvS", _env, _accessor);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        public async Task<Result> UpdatePictureAsync(string applicationUserId, UpdateUserProfilePictureRequest request, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);

            if (!string.IsNullOrEmpty(userProfile.ProfilePictureUrl))
            {
                FileHelper.DeleteFile(userProfile.ProfilePictureUrl, "Images", _env);
            }
            userProfile.ProfilePictureUrl = await FileHelper.UploadeFileAsync(request.ProfilePicture, "Images", _env, _accessor);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task <Result> UpdateSoftSkillsAsync(string applicationUserId, UpdateUserProfileSoftSkillsRequest request, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles.Include(up=>up.SoftSkills).FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);

            var newSkills = request.SoftSkills.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            var oldSkillIds = userProfile.SoftSkills.Select(s => s.ID).ToList();

            var skillsToDelete = await _context.SoftSkills.Where(s => oldSkillIds.Contains(s.ID)).ToListAsync(cancellationToken);
            _context.SoftSkills.RemoveRange(skillsToDelete);
            var newSoftSkills = newSkills.Select(skillName => new SoftSkill{Name = skillName,UserProfileId = userProfile.ID}).ToList();
            await _context.SoftSkills.AddRangeAsync(newSoftSkills, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        public async Task<Result> UpdateHardSkillsAsync(string applicationUserId, UpdateUserProfileHardSkillsRequest request, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles.Include(up => up.HardSkills).FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);

            var newSkills = request.HardSkills.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            var oldSkillIds = userProfile.HardSkills.Select(s => s.ID).ToList();

            var skillsToDelete = await _context.HardSkills.Where(s => oldSkillIds.Contains(s.ID)).ToListAsync(cancellationToken);
            _context.HardSkills.RemoveRange(skillsToDelete);
            var newSoftSkills = newSkills.Select(skillName => new HardSkill { Name = skillName, UserProfileId = userProfile.ID }).ToList();
            await _context.HardSkills.AddRangeAsync(newSoftSkills, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> DeleteCvAsync(string applicationUserId, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);
            if (userProfile.CvFileUrl is null)
                return Result.Failure(UserErrors.FileNotFound);
            FileHelper.DeleteFile(userProfile.CvFileUrl, "CvS", _env);
            userProfile.CvFileUrl = null;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        public async Task<Result> DeletePictureAsync(string applicationUserId, CancellationToken cancellationToken = default)
        {
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(up => up.ApplicationUserId == applicationUserId);
            if (userProfile is null)
                return Result.Failure(UserErrors.ProfileNotFound);
            if (userProfile.ProfilePictureUrl is null)
                return Result.Failure(UserErrors.FileNotFound);
            FileHelper.DeleteFile(userProfile.ProfilePictureUrl, "Images", _env);
            userProfile.ProfilePictureUrl = null;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
