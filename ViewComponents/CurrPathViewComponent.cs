using ExamPortal.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ExamPortal.ViewComponents
{
    public enum EPath
    {
        DashBoard, PaperList, MakeDiscriptivePaper,
        MakeMCQPaper, Login, AnswerSheetList, PaperCode,
        MCQPaperDetail, DescriptivePaperDetail, MCQAnswerSheetDetail,
        DescriptiveAnswerSheetDetail, MCQPaperResponses, DescriptivePaperResponses
    }
    public class PathItem
    {
        public PathItem(string display, string action, string controller)
        {
            this.Display = display;
            this.Action = action;
            this.Controller = controller;
        }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Display { get; set; }
    }

    public class CurrPathViewComponent : ViewComponent
    {
        Dictionary<EPath, PathItem> GetPathItem = new Dictionary<EPath, PathItem>()
        {
            {EPath.DashBoard,new PathItem("Dashboard",nameof(HomeController.Index),"home")},
            {EPath.PaperList,new PathItem("Paper List",nameof(TeacherController.Index),"teacher") },
            {EPath.MakeDiscriptivePaper,new PathItem("Make Discriptive Paper",nameof(TeacherController.MakeDescriptivePaper),"teacher") },
            {EPath.MakeMCQPaper,new PathItem("Make MCQ Paper",nameof(TeacherController.MakePaper),"teacher") },
            {EPath.PaperCode,new PathItem("Paper Code",nameof(StudentController.PaperCode),"student") },
            {EPath.AnswerSheetList,new PathItem("AnswerSheet List",nameof(StudentController.Index),"student") },
            {EPath.MCQPaperDetail,new PathItem("MCQ Paper Detail","","") },
            {EPath.Login,new PathItem("Login","","") },
            {EPath.DescriptivePaperDetail,new PathItem("Descriptive Paper Detail","","") },
            {EPath.MCQAnswerSheetDetail,new PathItem("MCQ AnswerSheet Detail","","") },
            {EPath.DescriptiveAnswerSheetDetail,new PathItem("Descriptive AnswerSheet Detail","","") },
            {EPath.MCQPaperResponses,new PathItem("MCQ Paper Responses","","") },
            {EPath.DescriptivePaperResponses,new PathItem("Descriptive Paper Responses","","") },

        };
        public IViewComponentResult Invoke(string header, List<EPath> paths)
        {
            List<PathItem> items = new List<PathItem>();
            foreach (var item in paths)
                items.Add(GetPathItem[item]);
            return View((header, items));
        }
    }
}
