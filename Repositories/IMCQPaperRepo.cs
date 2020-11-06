using ExamPortal.Models;
using ExamPortal.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.Repositories
{
    public interface IMCQPaperRepo
    {
        public MCQPaper GetByPaperCode(string paperCode);
        public KeyValuePair<IEnumerable<MCQPaper>, int> GetByTeacherEmail(string email,int page);
        public Task<MCQPaper> Create(MCQPaper paper);
    }

    public class MCQPaperRepoImpl : IMCQPaperRepo
    {
        public MCQPaperRepoImpl(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        private AppDbContext AppDbContext { get; }

        public async Task<MCQPaper> Create(MCQPaper paper)
        {
            List<MCQOption> temp = new List<MCQOption>();
            foreach (var que in paper.Questions)
            {
                temp.Add(que.TrueAnswer);
                que.TrueAnswer = null;
            }
            await using var transaction = await AppDbContext.Database.BeginTransactionAsync();
            try
            {
                AppDbContext.Add(paper);
                AppDbContext.SaveChanges();
                var i = 0;
                foreach (var que in paper.Questions)
                    que.TrueAnswer = temp[i++];
                AppDbContext.MCQPapers.Attach(paper);
                AppDbContext.SaveChanges();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }
            return paper;
        }

        public MCQPaper GetByPaperCode(string paperCode)
        {
            using var transaction = AppDbContext.Database.BeginTransaction();
            MCQPaper ans = new MCQPaper();
            try
            {
                ans = AppDbContext.MCQPapers
                .FirstOrDefault(paper => paper.PaperCode.Equals(paperCode));
                var questions = AppDbContext.MCQQuestions
                    .Include(que => que.MCQOptions)
                    .Where(que => que.MCQPaperId == ans.Id);
                ans.Questions = questions.ToList();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
            return ans;
        }

        public KeyValuePair<IEnumerable<MCQPaper>,int> GetByTeacherEmail(string email,int page)
        {
            int maxRows=3;
            var ans = AppDbContext.MCQPapers.Where(paper => paper.TeacherEmailId.Equals(email)).Skip((page-1)*maxRows).Take(maxRows);
            double TotalPages = (double)((decimal)AppDbContext.MCQPapers.Count() / Convert.ToDecimal(maxRows));
            KeyValuePair<IEnumerable<MCQPaper>, int> pair = new KeyValuePair<IEnumerable<MCQPaper>, int>(ans, (int)Math.Ceiling(TotalPages));
            return pair;
        }
    }

}
