using ExamPortal.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamPortal.Views.Shared.Components.StudentAnalysis
{
    public class StudentAnalysisViewComponent : ViewComponent
    {
        public StudentAnalysisViewComponent(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public AppDbContext DbContext { get; set; }
        public IViewComponentResult Invoke()
        {
            var ls = DbContext.AnswerSheets
                .Where(sheet => sheet.StudentEmailId.Equals(User.Identity.Name) && sheet.MarksObtained > 0)
                .OrderByDescending(sheet => sheet.SubmittedTime)
                .Select(sheet => new { obt = sheet.MarksObtained, id = sheet.AnswerSheetId })
                .Take(10).ToList();
            ls.Reverse();
            var Percentage = new List<double>();
            var title = new List<string>();
            foreach (var id in ls)
            {
                var x = DbContext.MCQAnswerSheets
                    .Include(sheet => sheet.MCQPaper)
                    .FirstOrDefault(sheet => sheet.AnswerSheetId.Equals(id.id));
                double val;
                if (x != null)
                {
                    val = (double)(id.obt * 100.0) / x.MCQPaper.TotalMarks;
                    title.Add(x.MCQPaper.PaperTitle);

                }
                else
                {
                    var y = DbContext.DescriptiveAnswerSheets
                        .Include(sheet => sheet.DescriptivePaper)
                        .FirstOrDefault(sheet => sheet.AnswerSheetId.Equals(id.id));
                    val = (double)(id.obt * 100.0) / y.DescriptivePaper.TotalMarks;
                    title.Add(y.DescriptivePaper.PaperTitle);
                }
                val = Math.Round(val, 2);
                Percentage.Add(val);
            }
            return View((title, Percentage));
        }
    }
}
