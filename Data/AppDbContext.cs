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
                .OnDelete(DeleteBehavior.Restrict);  // Change to Restrict

            // One-to-Many: Intern → Swipes
            modelBuilder.Entity<Swipe>()
                .HasOne(s => s.Intern)
                .WithMany()
                .HasForeignKey(s => s.InternId)
                .OnDelete(DeleteBehavior.Restrict);  // Change to Restrict

            // ✅ Configure Many-to-One Relationship (Interns ↔ Field)
            modelBuilder.Entity<Intern>()
                .HasOne(i => i.Field)       // Each Intern has one Field
                .WithMany(f => f.Interns)   // Each Field can have many Interns
                .HasForeignKey(i => i.FieldId) // Foreign Key in Intern table
                .OnDelete(DeleteBehavior.NoAction); // Prevent cascading deletes


            modelBuilder.Entity<AccountType>().HasData(
                    new AccountType { Id = 1, TypeName = "Intern" },
                    new AccountType { Id = 2, TypeName = "Recruiter" }
            );

            modelBuilder.Entity<Field>().HasData(
                    new Field { Id = 2, Name= "Healthcare" },
                    new Field { Id = 1, Name= "Technology" },
                    new Field { Id = 3, Name= "Finance" },
                    new Field { Id = 4, Name= "Education" },
                    new Field { Id = 5, Name= "Marketing" },
                    new Field { Id = 6, Name= "Engineering" },
                    new Field { Id = 7, Name= "Creative Arts" },
                    new Field { Id = 8, Name= "Human Resources" },
                    new Field { Id = 9, Name= "Legal & Laws" },
                    new Field { Id = 10,Name = "Science & Research" }
            );
        }
    }
}
