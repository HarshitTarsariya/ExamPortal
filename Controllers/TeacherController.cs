using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ExamPortal.DTOS;
using ExamPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExamPortal.Controllers
{
    public class TeacherController : Controller
    {
        public TeacherController(ITeacherService teacherService)
        {
            TeacherService = teacherService;
        }

        public ITeacherService TeacherService { get; }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult MakePaper()
        {
            return View();
        }
        public IActionResult MakePaper(MCQPaperDTO paper)
        {
            paper.TeacherEmailId = User.Identity.Name;

            TeacherService.CreatePaper(paper);
            return RedirectToAction("index", "home");
        }
    }
}