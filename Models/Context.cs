using Microsoft.EntityFrameworkCore;

namespace Grading_System_Backend.Models
{
    public class Context:DbContext
    {
        DbSet<Student> Students { get; set; }
        DbSet<Subject> Subjects { get; set; }
        DbSet<StudentSubjects> StudentSubjects { get; set; }
        DbSet<AcademicYear> academicYears { get; set; }

        public Context() { }

        public Context(DbContextOptions options) :base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<Student>()
                .HasIndex(e => e.NationalId)
                .IsUnique();

            modelBuilder.Entity<Student>()
            .HasMany(s => s.StudentSubjects)
            .WithOne(s => s.Student)
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
