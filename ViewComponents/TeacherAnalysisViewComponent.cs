using ExamPortal.DTOS;
using ExamPortal.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamPortal.ViewComponents
{
    public class TeacherAnalysisViewComponent:ViewComponent 
    {
        public TeacherAnalysisViewComponent(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public AppDbContext DbContext { get; set; }
        public IViewComponentResult Invoke()
        {
            var listofPapers = DbContext.Papers.Where(paper => paper.TeacherEmailId.Equals(User.Identity.Name)).OrderByDescending(paper => paper.CreatedDate).Select(paper=> new {title= paper.PaperTitle,code=paper.PaperCode}).Take(10).ToList();
            var listofTitle = new List<string>();
            var listofCount = new List<int>();
            System.Diagnostics.Debug.Print("Hello Chat");
            foreach(var paper in listofPapers)
            {
                if (CodeGenerator.GetPaperType(paper.code) == EPaperType.MCQ)
                    listofCount.Add(DbContext.MCQAnswerSheets.Where(sheet => sheet.MCQPaper.PaperCode.Equals(paper.code)).Count());
                else
                    listofCount.Add(DbContext.DescriptiveAnswerSheets.Where(sheet => sheet.DescriptivePaper.PaperCode.Equals(paper.code)).Count());
                listofTitle.Add(paper.title);
            }
          
            return View((listofTitle, listofCount));
            
        }
    }
}
