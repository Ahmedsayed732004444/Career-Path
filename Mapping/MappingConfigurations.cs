

using Career_Path.Contracts.UserProfile;

namespace Career_Path.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
       

        config.NewConfig<RegisterRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email);

        config.NewConfig<UserProfile, UserProfileResponse>()
             .Map(dest => dest.FullName, src => $"{src.ApplicationUser.FirstName} {src.ApplicationUser.LastName}")
             .Map(dest => dest.Email, src => src.ApplicationUser.Email)
             .Map(dest => dest.SoftSkills, src => src.SoftSkills.Select(s => s.Name))
             .Map(dest => dest.HardSkills, src => src.HardSkills.Select(h => h.Name));

        // Mapping for UserProfileBasicRequest to UserProfile
        config.NewConfig<UserProfileBasicRequest, UserProfile>()
            .Ignore(dest => dest.ApplicationUser)
            .Ignore(dest => dest.ApplicationUserId)
            .Ignore(dest => dest.ID)
            .Ignore(dest => dest.SoftSkills)
            .Ignore(dest => dest.HardSkills)
            .Ignore(dest => dest.ProfilePictureUrl)
            .Ignore(dest => dest.CvFileUrl);

        // Mapping for UserProfileRequest to UserProfile
        config.NewConfig<UserProfileRequest, UserProfile>()
            .Map(dest => dest.SoftSkills, src => src.SoftSkills.Select(s => new SoftSkill { Name = s }))
            .Map(dest => dest.HardSkills, src => src.HardSkills.Select(h => new HardSkill { Name = h }))
            .Ignore(dest => dest.ID)
            .Ignore(dest => dest.ApplicationUserId)
            .Ignore(dest => dest.ApplicationUser)
            .Ignore(dest => dest.ProfilePictureUrl)
            .Ignore(dest => dest.CvFileUrl);

    }


}


