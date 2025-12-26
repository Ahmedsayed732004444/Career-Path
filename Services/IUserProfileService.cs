using Career_Path.Contracts.UserProfile;

namespace Career_Path.Services
{
    public interface IUserProfileService
    {
        Task<UserProfileResponse> GetAsync(string applicationUserId);
        Task<Result> AddProfileAsync(string applicationUserId, UserProfileRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateBasicDataAsync(string applicationUserId, UserProfileBasicRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateSoftSkillsAsync(string applicationUserId, UpdateUserProfileSoftSkillsRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateHardSkillsAsync(string applicationUserId, UpdateUserProfileHardSkillsRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateCvAsync(string applicationUserId, UpdateUserProfileCvRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdatePictureAsync(string applicationUserId, UpdateUserProfilePictureRequest request, CancellationToken cancellationToken = default);
        Task<Result> DeleteCvAsync(string applicationUserId, CancellationToken cancellationToken = default);
        Task<Result> DeletePictureAsync(string applicationUserId, CancellationToken cancellationToken = default);
    }
}
