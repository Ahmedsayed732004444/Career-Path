namespace Career_Path.Entities
{
    public enum UserGender
    {
        Male,
        Female
    }
    public class UserProfile
    {
        public int ID { get; set; }
        public string ApplicationUserId { get; set; }
        public UserGender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string JobTitle { get; set; }
        public string University { get; set; }
        public string Degree { get; set; }
        public int YearsOfExperience { get; set; }
        public decimal? SalaryExpectations { get; set; }
        public string? Summary { get; set; }
        public int? GraduationYear { get; set; }
        public string? Company { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string?  CvFileUrl { get; set; }
        public virtual ICollection<SoftSkill> SoftSkills { get; set; }= new List<SoftSkill>();
        public virtual ICollection<HardSkill> HardSkills { get; set; }= new List<HardSkill>();
        public ApplicationUser ApplicationUser { get; set; }
    }
}

