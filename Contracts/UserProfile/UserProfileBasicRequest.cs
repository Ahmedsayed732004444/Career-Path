namespace Career_Path.Contracts.UserProfile
{
    public record UserProfileBasicRequest
    (
        string FirstName,
        string LastName,
        DateOnly DateOfBirth,
        UserGender Gender,
        string JobTitle,
        string Country,
        string City,
        string University,
        string Degree,
        int YearsOfExperience,
        decimal? SalaryExpectations,
        string? Summary,
        int? GraduationYear,
        string? Company
     );
}
