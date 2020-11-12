using ExamPortal.Models;
using ExamPortal.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ExamPortal.Repositories
{
    public interface IDescriptivePaperRepo
    {
        public DescriptivePaper GetByPaperCode(string paperCode);
        public IEnumerable<DescriptivePaper> GetByTeacherEmail(string email);
        public DescriptivePaper Create(DescriptivePaper paper);
        public void Delete(string code);
    }

    public class DescriptivePaperRepoImpl : IDescriptivePaperRepo
    {
        public DescriptivePaperRepoImpl(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        private AppDbContext AppDbContext { get; }
        public DescriptivePaper GetByPaperCode(string paperCode)
        {
            return AppDbContext.DescriptivePapers
                .FirstOrDefault(paper => paper.PaperCode.Equals(paperCode));
        }
        public DescriptivePaper Create(DescriptivePaper paper)
        {
            AppDbContext.DescriptivePapers.Add(paper);
            AppDbContext.SaveChanges();
            return paper;
        }
        public IEnumerable<DescriptivePaper> GetByTeacherEmail(string email)
        {
            return AppDbContext.DescriptivePapers.Where(paper => paper.TeacherEmailId.Equals(email));
        }

        public void Delete(string code)
        {
            AppDbContext.DescriptivePapers.Remove(AppDbContext.DescriptivePapers.Where(p => p.PaperCode.Equals(code)).FirstOrDefault());
            AppDbContext.SaveChanges();
        }
    }

}
