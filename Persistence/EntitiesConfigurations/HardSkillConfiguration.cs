namespace Career_Path.Persistence.EntitiesConfigurations
{
    public class HardSkillConfiguration : IEntityTypeConfiguration<HardSkill>
    {
        public void Configure(EntityTypeBuilder<HardSkill> builder)
        {

            // Properties
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(x => x.UserProfile)
                .WithMany(u => u.HardSkills)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("HardSkills");
        }
    }
}