namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.JobTitle)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.University)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Degree)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Gender)
                .IsRequired()
                .HasConversion<string>(); // تخزين الـ enum كـ string

            builder.Property(x => x.DateOfBirth)
                .IsRequired();

            builder.Property(x => x.YearsOfExperience)
                .IsRequired();

            // Optional Properties
            builder.Property(x => x.Company)
                .HasMaxLength(100);

            builder.Property(x => x.Summary)
                .HasMaxLength(1000);

            builder.Property(x => x.ProfilePictureUrl)
                .HasMaxLength(500);

            builder.Property(x => x.CvFileUrl)
                .HasMaxLength(500);

            builder.Property(x => x.SalaryExpectations)
                .HasPrecision(18, 2); // للأرقام العشرية

            // Relationships
            builder.HasOne(x => x.ApplicationUser)
                .WithOne(u => u.UserProfile)
                .HasForeignKey<UserProfile>(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.SoftSkills)
                .WithOne(s => s.UserProfile)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.HardSkills)
                .WithOne(h => h.UserProfile)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(x => x.ApplicationUserId)
                .IsUnique();

            // Table Name
            builder.ToTable("UserProfiles");
        }
    }
}