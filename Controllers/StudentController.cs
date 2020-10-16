using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamPortal.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using AutoMapper;
using ExamPortal.Services;
using ExamPortal.Models;
using ExamPortal.Utilities;

namespace ExamPortal.Controllers
{
    public class StudentController : Controller
    {
        public StudentController(IMapper mapper,IStudentService studentService,ITeacherService teacherService)
        {
            Mapper = mapper;
            StudentService = studentService;
            TeacherService = teacherService;
        }

        public IStudentService StudentService { get; }
        public ITeacherService TeacherService { get; }
        IMapper Mapper { get; set; }


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
            MCQPaper paper=StudentService.GetMcqPaper(papercode);
            if (paper == null)
            {
                ViewBag.Data = "Invalid Paper code";
                return View();
            }
            MCQPaperDTO paperdto = Change(paper);
            MCQAnswerSheet paper1 = TeacherService.GetMCQAnswerSheetByCodeAndEmail(papercode, User.Identity.Name);
            if (paper1 != null)
            {
                ViewBag.Data = "You have Already Submitted the Paper Once , if Required Contact Paper Setter - "+paper.TeacherEmailId;
                return View();
            }
            ViewBag.User = User.Identity.Name;
            return View("Verify", paperdto);
        }
        [HttpPost]
        public IActionResult GetPaper(string id)
        {
            MCQPaper paper = StudentService.GetMcqPaper(id);
            MCQPaperDTO paperdto = Change(paper);
            return View(paperdto);
        }
        [HttpPost]
        public IActionResult SubmitPaper(MCQPaperDTO mCQPaperDTO)
        {
            KeyValuePair<int, int> marks = TeacherService.SetMCQAnswerSheet(mCQPaperDTO, User.Identity.Name);
            ViewBag.MarksObtained = marks.Value;
            ViewBag.TotalMarks = marks.Key;
            ViewBag.Message = GetMessage(marks.Key, marks.Value);
            ViewBag.User = User.Identity.Name;
            return View();
        }
        MCQPaperDTO Change(MCQPaper paper)
        {
            MCQPaperDTO paperdto = new MCQPaperDTO();
            Mapper.Map(paper, paperdto);
            foreach (var que in paper.Questions)
                paperdto.Questions.Add(que.EntityToDto());
            return paperdto;
        }


        /*--------------------------------------------------------------------------*/



        string GetMessage(int total,int obtained)
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
        }
    }
}