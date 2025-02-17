using Microsoft.EntityFrameworkCore;
using api.Entities;

namespace api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Intern> Interns { get; set; }
        public DbSet<Recruit> Recruits { get; set; }
        public DbSet<Swipe> Swipes { get; set; }
        public DbSet<InternEducation> InternEducations { get; set; }
        public DbSet<InternWorkExperience> InternWorkExperiences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasOne(a => a.AccountType)
                .WithMany(at => at.Accounts)
                .HasForeignKey(a => a.AccountTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: Account → Interns
            modelBuilder.Entity<Intern>()
                .HasOne(i => i.Account)
                .WithMany(a => a.Interns)
                .HasForeignKey(i => i.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            // One-to-Many: Account → Recruits
            modelBuilder.Entity<Recruit>()
                .HasOne(r => r.Account)
                .WithMany(a => a.Recruits)
                .HasForeignKey(r => r.AccountId)
                .OnDelete(DeleteBehavior.NoAction);

            // One-to-Many: Recruit → Swipes
            modelBuilder.Entity<Swipe>()
                .HasOne(s => s.Recruit)
                .WithMany(r => r.Swipes)
                .HasForeignKey(s => s.RecruitId)
                .OnDelete(DeleteBehavior.Restrict);  // 🔴 Change to Restrict

            // One-to-Many: Intern → Swipes
            modelBuilder.Entity<Swipe>()
                .HasOne(s => s.Intern)
                .WithMany()
                .HasForeignKey(s => s.InternId)
                .OnDelete(DeleteBehavior.Restrict);  // 🔴 Change to Restrict

            // ✅ Correct Many-to-Many Configuration (Intern ↔ Field)
            modelBuilder.Entity<Intern>()
            .HasMany(i => i.Fields)
            .WithMany(f => f.Interns)
            .UsingEntity<Dictionary<string, object>>(
                  "InternFieldMapping", // Join Table Name
                  j => j.HasOne<Field>()
                        .WithMany()
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.NoAction),
                  j => j.HasOne<Intern>()
                        .WithMany()
                        .HasForeignKey("InternId")
                        .OnDelete(DeleteBehavior.NoAction)
            );


            modelBuilder.Entity<Intern>()
                .HasMany(i => i.InternEducations)
                .WithMany(e => e.Interns)
                .UsingEntity<Dictionary<string, object>>(
                    "InternEducationMapping", // Join Table Name
                    j => j.HasOne<InternEducation>()
                          .WithMany()
                          .HasForeignKey("InternEducationId")
                          .OnDelete(DeleteBehavior.NoAction),
                    j => j.HasOne<Intern>()
                          .WithMany()
                          .HasForeignKey("InternId")
                          .OnDelete(DeleteBehavior.NoAction)
                );
                // Intern ↔ InternWorkExperience (One-to-Many)
            modelBuilder.Entity<InternWorkExperience>()
                  .HasOne(iwe => iwe.Intern)
                  .WithMany(i => i.WorkExperiences)
                  .HasForeignKey(iwe => iwe.InternId)
                  .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
