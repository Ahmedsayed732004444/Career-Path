namespace Career_Path.Contracts.UserProfile
{
    public record UserProfileResponse
    (
        string FullName,
        string Email,
        string? ProfilePictureUrl,
        UserGender Gender,
        DateOnly DateOfBirth,
        string JobTitle,
        string Country,
        string City,   
        string University,
        string Degree,
        int YearsOfExperience,
        decimal? SalaryExpectations,
        string? Summary,
        int? GraduationYear,
        string? Company,
        string? CvFileUrl,
        ICollection<string> SoftSkills,
        ICollection<string> HardSkills
    );
}
