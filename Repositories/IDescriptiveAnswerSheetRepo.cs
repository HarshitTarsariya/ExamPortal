using ExamPortal.Models;
using ExamPortal.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace ExamPortal.Repositories
{
    public interface IDescriptiveAnswerSheetRepo
    {
        public DescriptiveAnswerSheet GetByPaperCodeAndStudentEmail(string PaperCode, string StudentEmailId);
        public IEnumerable<DescriptiveAnswerSheet> GetByStudentEmail(string StudentEmailId);
        public void SetDescriptiveAnswerSheet(DescriptiveAnswerSheet descriptiveAnswerSheet);
        public IEnumerable<DescriptiveAnswerSheet> GetAllResponseByCode(string papercode);
        public void SetMarksInDescriptivePaper(string papercode, int marksgiven, string studentname);

    }
    public class DescriptiveAnswerSheetRepoImpl : IDescriptiveAnswerSheetRepo
    {
        #region Constructor and Properties
        public DescriptiveAnswerSheetRepoImpl(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public AppDbContext DbContext { get; }
        #endregion
        public DescriptiveAnswerSheet GetByPaperCodeAndStudentEmail(string PaperCode, string StudentEmailId)
        {
            return DbContext.DescriptiveAnswerSheets
                .FirstOrDefault(ele => ele.DescriptivePaper.PaperCode == PaperCode && ele.StudentEmailId == StudentEmailId);
        }

        public IEnumerable<DescriptiveAnswerSheet> GetByStudentEmail(string StudentEmailId)
        {
            return DbContext.DescriptiveAnswerSheets.Where(ele => ele.StudentEmailId == StudentEmailId);
        }
        public void SetDescriptiveAnswerSheet(DescriptiveAnswerSheet descriptiveAnswerSheet)
        {
            DbContext.DescriptiveAnswerSheets.Add(descriptiveAnswerSheet);
            DbContext.SaveChanges();
        }
        public IEnumerable<DescriptiveAnswerSheet> GetAllResponseByCode(string papercode)
        {
            return DbContext.DescriptiveAnswerSheets.Where(paper => paper.DescriptivePaper.PaperCode.Equals(papercode));
        }
        public void SetMarksInDescriptivePaper(string papercode, int marksgiven, string studentname)
        {
            var answer = DbContext.DescriptiveAnswerSheets.FirstOrDefault(paper => paper.DescriptivePaper.PaperCode.Equals(papercode) && paper.StudentEmailId.Equals(studentname));
            answer.MarksObtained = marksgiven;
            DbContext.SaveChanges();
        }
    }
}
