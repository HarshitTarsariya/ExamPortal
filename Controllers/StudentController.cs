using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPortal.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using ExamPortal.Models;
using ExamPortal.Services;
using System.Threading;

namespace ExamPortal.Controllers
{
    public class StudentController : Controller
    {
        public IStudentService StudentService { get; }
        public ITeacherService TeacherService { get; }

        public StudentController(IStudentService studentService, ITeacherService teacherService)
        {
            StudentService = studentService;
            TeacherService = teacherService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult PaperCode()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PaperCode(string papercode)
        {
            MCQPaperDTO paperdto = StudentService.GetMcqPaper(papercode);
            if (paperdto == null)
            {

                ViewBag.Data = "Invalid Paper code";
                return View();
            }
            MCQAnswerSheet paper1 = TeacherService.GetMCQAnswerSheetByCodeAndEmail(papercode, User.Identity.Name);
            if (paper1 != null)
            {
                ViewBag.Data = "You have Already Submitted the Paper Once , if Required Contact Paper Setter - " + paperdto.TeacherEmailId;
                return View();
            }
            ViewBag.User = User.Identity.Name;
            return View("Verify", paperdto);
        }
        public IActionResult GetPaper(string id)
        {
            MCQPaperDTO paperdto = StudentService.GetMcqPaper(id);
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            return View(paperdto);
        }

        public IActionResult GetResults()
        {
            return View(StudentService.GetAnswerSheets(User.Identity.Name));
        }

        [HttpPost]
        public IActionResult SubmitPaper(MCQPaperDTO mCQPaperDTO)
        {
            Func<int, int, string> GetMessage = (total, obtained) =>
            {
                double per = (obtained * 100.0) / (total);
                string msg;
                if (per > 90.0)
                    msg = "Excellent Work!!";
                else if (per > 80.0)
                    msg = "Great Going !! Keep it up";
                else if (per > 70.0)
                    msg = "Good Progress , Require extra practise";
                else if (per > 50.0)
                    msg = "Ahh Quite less Marks , Require regular practise , Don't pressurise Practise makes man perfect!!";
                else
                    msg = "Requires Special Meeting with Faculty";
                return msg;
            };
            KeyValuePair<int, int> marks = TeacherService.SetMCQAnswerSheet(mCQPaperDTO, User.Identity.Name);
            ViewBag.MarksObtained = marks.Value;
            ViewBag.TotalMarks = marks.Key;
            ViewBag.Message = GetMessage(marks.Key, marks.Value);
            ViewBag.User = User.Identity.Name;
            return View();
        }
    }
}