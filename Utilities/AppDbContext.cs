using ExamPortal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExamPortal.Utilities
{
    //Db context class for interacting in Database
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Paper> Papers { get; set; }
        public DbSet<MCQPaper> MCQPapers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<MCQQuestion> MCQQuestions { get; set; }
        public DbSet<AnswerSheet> AnswerSheets { get; set; }
        public DbSet<MCQAnswerSheet> MCQAnswerSheets { get; set; }
        public DbSet<MCQOption> MCQOptions { get; set; }

        public DbSet<DescriptivePaper> DescriptivePapers { get; set; }
        public DbSet<DescriptiveAnswerSheet> DescriptiveAnswerSheets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MCQOption>()
                .HasOne(x => x.MCQQuestion)
                .WithMany(x => x.MCQOptions)
                .HasForeignKey(x => x.MCQQuestionId);
        }
    }
}
