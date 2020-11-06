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

        public IActionResult MyPapers(int? page)
        {
            var pair = TeacherService.getPapersByEmailId(User.Identity.Name,page??1);
            var data = pair.Key;
            ViewBag.pagecount = pair.Value;
            ViewBag.currentpage = page ?? 1;
            return View(data);
        }

        public IActionResult PaperDetails(string id)
        {
            var data = TeacherService.getPaperByCode(id);
            if(data.TeacherEmailId != User.Identity.Name)
            {
                return Redirect("/Error/403");
            }
            else
            {
                return View(data);
            }
            //return Json(data);
            
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