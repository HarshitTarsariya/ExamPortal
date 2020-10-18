using ExamPortal.Models;
using ExamPortal.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Repositories
{
    public interface IMCQAnswerSheetRepo
    {
        public MCQAnswerSheet GetByPaperCodeAndStudentEmail(string PaperCode, string StudentEmailId);
        public IEnumerable<MCQAnswerSheet> GetByStudentEmail(string StudentEmailId);
        public void SetMCQAnswerSheet(MCQAnswerSheet answerSheet);
    }

    public class MCQAnswerSheetRepoImpl : IMCQAnswerSheetRepo
    {
        public MCQAnswerSheetRepoImpl(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        private AppDbContext AppDbContext { get; }

        public MCQAnswerSheet GetByPaperCodeAndStudentEmail(string PaperCode, string StudentEmailId)
        {
            return AppDbContext.MCQAnswerSheets
                .Where(ans => ans.MCQPaper.PaperCode.Equals(PaperCode) && ans.StudentEmailId.Equals(StudentEmailId))
                .FirstOrDefault();
        }

        public IEnumerable<MCQAnswerSheet> GetByStudentEmail(string StudentEmailId)
        {
            return AppDbContext.MCQAnswerSheets
                .Include(ans => ans.MCQPaper)
                .ThenInclude(x => x.Questions)
                .Where(ans => ans.StudentEmailId.Equals(StudentEmailId))
                .ToList();
        }
        public void SetMCQAnswerSheet(MCQAnswerSheet answerSheet)
        {
            AppDbContext.Add(answerSheet);
            AppDbContext.SaveChanges();
        }
    }
}
