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
        public IEnumerable<MCQPaper> GetByTeacherEmail(string email);
        public Task<MCQPaper> Create(MCQPaper paper);
        public void Delete(string papercode);
        public Tuple<int, IEnumerable<Paper>> getAllPapersByEmailId(string emailId, int page);
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
            using (var transaction = await AppDbContext.Database.BeginTransactionAsync())
            {
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
            }
            return paper;
        }

        public void Delete(string papercode)
        {
            AppDbContext.MCQPapers.Remove(AppDbContext.MCQPapers.Where(p => p.PaperCode.Equals(papercode)).FirstOrDefault());
            AppDbContext.SaveChanges();
        }

        public MCQPaper GetByPaperCode(string paperCode)
        {
            MCQPaper ans = null;
            using (var transaction = AppDbContext.Database.BeginTransaction())
            {
                try
                {
                    ans = AppDbContext.MCQPapers
                        .FirstOrDefault(paper => paper.PaperCode.Equals(paperCode));
                    if (ans == null)
                    {
                        transaction.Commit();
                        return null;
                    }
                    ans.Questions =
                        AppDbContext.MCQQuestions
                        .Include(que => que.TrueAnswer)
                        .Where(que => que.MCQPaper.PaperCode == paperCode)
                        .ToList();

                    foreach (var que in ans.Questions)
                    {
                        que.TrueAnswer = que.MCQOptions[0];
                        que.MCQOptions = AppDbContext.MCQOptions
                                        .Where(opt => opt.MCQQuestionId == que.QuestionId)
                                        .ToList();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
            return ans ?? new MCQPaper();
        }

        public IEnumerable<MCQPaper> GetByTeacherEmail(string email)
        {
            var ans = AppDbContext.MCQPapers.Where(paper => paper.TeacherEmailId.Equals(email));
            return ans;
        }
        public Tuple<int, IEnumerable<Paper>> getAllPapersByEmailId(string emailId, int page)
        {
            int rows = 5;
            var paper = AppDbContext.Papers.Where(paper => paper.TeacherEmailId.Equals(emailId)).Skip(rows * (page - 1)).Take(rows);
            var total = AppDbContext.Papers.Where(paper => paper.TeacherEmailId.Equals(emailId)).Count();
            double pageCount = (double)((decimal)total / Convert.ToDecimal(rows));
            return new Tuple<int, IEnumerable<Paper>>((int)Math.Ceiling(pageCount), paper);
        }
    }

}
