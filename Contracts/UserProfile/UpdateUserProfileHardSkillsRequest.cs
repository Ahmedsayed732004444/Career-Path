namespace Career_Path.Contracts.UserProfile
{
    public record UpdateUserProfileHardSkillsRequest
        (
            List<string> HardSkills
        );
}
