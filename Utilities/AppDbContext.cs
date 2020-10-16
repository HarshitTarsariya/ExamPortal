using ExamPortal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Utilities
{
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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
