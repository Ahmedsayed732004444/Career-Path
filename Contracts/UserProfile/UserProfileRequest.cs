namespace Career_Path.Contracts.UserProfile
{
    public record UserProfileRequest
    (
        IFormFile? ProfilePicture,
        UserGender Gender,
        string JobTitle,
        string Country,
        string City,   
        string University,
        string Degree,
        int YearsOfExperience,
        ICollection<string> SoftSkills,
        ICollection<string> HardSkills,
        DateOnly DateOfBirth,
        decimal? SalaryExpectations,
        string? Summary,
        int? GraduationYear,
        string? Company,
        IFormFile? CvFile
    );
}
