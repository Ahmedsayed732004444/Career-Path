namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class SoftSkillConfiguration : IEntityTypeConfiguration<SoftSkill>
    {
        public void Configure(EntityTypeBuilder<SoftSkill> builder)
        {
            // Properties
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(x => x.UserProfile)
                .WithMany(u => u.SoftSkills)
                .OnDelete(DeleteBehavior.Cascade);

            // Table Name
            builder.ToTable("SoftSkills");
        }
    }
}