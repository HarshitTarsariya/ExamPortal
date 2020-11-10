using ExamPortal.Models;
using ExamPortal.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            return AppDbContext.MCQAnswerSheets.Include(ans=>ans.MCQPaper)
                .Where(ans => ans.MCQPaper.PaperCode.Equals(PaperCode) && ans.StudentEmailId.Equals(StudentEmailId))
                .FirstOrDefault();
        }

        public IEnumerable<MCQAnswerSheet> GetByStudentEmail(string StudentEmailId)
        {
            var answersheets = AppDbContext.MCQAnswerSheets.Include(ans=>ans.MCQPaper).ToList();
            foreach (var sheet in answersheets)
            {
                sheet.MCQPaper.Questions = AppDbContext.MCQQuestions.Where(que => que.MCQPaperId == sheet.MCQPaperId).ToList();
            }
            return answersheets;
        }
        public void SetMCQAnswerSheet(MCQAnswerSheet answerSheet)
        {
            AppDbContext.Add(answerSheet);
            AppDbContext.SaveChanges();
        }
    }
}
