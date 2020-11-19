using ExamPortal.Models;
using ExamPortal.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamPortal.Repositories
{
    public interface IMCQAnswerSheetRepo
    {
        public MCQAnswerSheet GetByPaperCodeAndStudentEmail(string PaperCode, string StudentEmailId);
        public IEnumerable<MCQAnswerSheet> GetByStudentEmail(string StudentEmailId);
        public void SetMCQAnswerSheet(MCQAnswerSheet answerSheet);
        public IEnumerable<MCQAnswerSheet> GetByPaperCode(string papercode);
        public (int total, IEnumerable<AnswerSheet> answersheet) GetAllAnswerSheets(string StudentEmailId, int page);
        public Paper GetPaperByAnswerSheetId(int answersheetid);
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
            return AppDbContext.MCQAnswerSheets.Include(ans => ans.MCQPaper)
                .Where(ans => ans.MCQPaper.PaperCode.Equals(PaperCode) && ans.StudentEmailId.Equals(StudentEmailId))
                .FirstOrDefault();
        }

        public IEnumerable<MCQAnswerSheet> GetByStudentEmail(string StudentEmailId)
        {
            IEnumerable<MCQAnswerSheet> answersheets = new List<MCQAnswerSheet>();
            using (var transaction = AppDbContext.Database.BeginTransaction())
            {
                try
                {
                    answersheets = AppDbContext.MCQAnswerSheets
                                       .Include(ans => ans.MCQPaper)
                                       .Where(ans => ans.StudentEmailId == StudentEmailId)
                                       .ToList();
                    foreach (var sheet in answersheets)
                    {
                        sheet.MCQPaper.Questions = AppDbContext.MCQQuestions.Where(que => que.MCQPaperId == sheet.MCQPaperId).ToList();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return answersheets;
        }
        public IEnumerable<MCQAnswerSheet> GetByPaperCode(string papercode)
        {
            return AppDbContext.MCQAnswerSheets.Where(
                ans => ans.MCQPaperId == AppDbContext.MCQPapers.FirstOrDefault(paper => paper.PaperCode == papercode).PaperId
            ).ToList();
        }
        public void SetMCQAnswerSheet(MCQAnswerSheet answerSheet)
        {
            AppDbContext.Add(answerSheet);
            AppDbContext.SaveChanges();
        }
        public (int total, IEnumerable<AnswerSheet> answersheet) GetAllAnswerSheets(string StudentEmailId, int page)
        {
            int rows = 5;
            var ans = AppDbContext.AnswerSheets.Where(sheet => sheet.StudentEmailId.Equals(StudentEmailId)).Skip((page - 1) * rows).Take(rows);
            var total = AppDbContext.AnswerSheets.Where(sheet => sheet.StudentEmailId.Equals(StudentEmailId)).Count();
            double pageCount = (double)((decimal)total / Convert.ToDecimal(rows));
            return ((int)Math.Ceiling(pageCount), ans);
        }
        public Paper GetPaperByAnswerSheetId(int answersheetid)
        {
            var p1 = AppDbContext.MCQAnswerSheets.Include(x => x.MCQPaper).FirstOrDefault(sheet => sheet.AnswerSheetId == answersheetid);
            if (p1 != null)
                return p1.MCQPaper;
            var p2 = AppDbContext.DescriptiveAnswerSheets.Include(x => x.DescriptivePaper).FirstOrDefault(sheet => sheet.AnswerSheetId == answersheetid);
            if (p2 != null)
                return p2.DescriptivePaper;

            return null;
        }
    }
}
