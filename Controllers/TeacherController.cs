using ExamPortal.DTOS;
using ExamPortal.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExamPortal.Controllers
{
    public class TeacherController : Controller
    {
        public TeacherController(ITeacherService teacherService) => TeacherService = teacherService;

        public ITeacherService TeacherService { get; }

        public IActionResult Index() => View();
        [HttpGet]
        public IActionResult MakePaper() => View();
        public IActionResult MakePaper(MCQPaperDTO paper)
        {
            paper.TeacherEmailId = User.Identity.Name;
            //return Json(paper);
            TeacherService.CreatePaper(paper);
            return RedirectToAction("index", "home");
        }

        public IActionResult MyPapers()
        {
            var data = TeacherService.getPapersByEmailId(User.Identity.Name);
            return View(data);
        }

        public IActionResult PaperDetails(string papercode)
        {
            var data = TeacherService.getPaperByCode(papercode);
            //return Json(data);
            return View(data);
        }

        [HttpGet]
        public IActionResult MakeDescriptivePaper()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MakeDescriptivePaper(DescriptivePaperDTO DesPaper)
        {
            DesPaper.TeacherEmailId = User.Identity.Name;
            await TeacherService.CreateDescriptivePaper(DesPaper);
            return RedirectToAction("index", "home");
        }
    }
}