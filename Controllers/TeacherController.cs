using ExamPortal.DTOS;
using ExamPortal.Services;
using ExamPortal.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExamPortal.Controllers
{
    public class TeacherController : Controller
    {
        #region Constructor and Properties
        public TeacherController(ITeacherService teacherService) => TeacherService = teacherService;

        public ITeacherService TeacherService { get; }
        #endregion


        public IActionResult Index() => View();
        [HttpGet]
        public IActionResult MakePaper() => View();
        [HttpPost]
        public IActionResult MakePaper(MCQPaperDTO paper)
        {
            if (ModelState.IsValid)
            {
                paper.TeacherEmailId = User.Identity.Name;
                //return Json(paper);
                TeacherService.CreateMCQPaper(paper);
                return RedirectToAction(nameof(MyPapers), "teacher");
            }
            return View(paper);
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
            return RedirectToAction(nameof(HomeController.Index), "home");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePaper(string paperCode)
        {
            await TeacherService.deletePaper(paperCode);
            return RedirectToAction(nameof(MyPapers));
        }
        [HttpGet]
        public IActionResult MyPapers()
        {
            var data = TeacherService.getMCQPapersByEmailId(User.Identity.Name);
            return View(data);
        }
        [HttpGet]
        public IActionResult PaperDetails(string papercode)
        {
            if (CodeGenerator.GetPaperType(papercode) == EPaperType.MCQ)
            {
                var data = TeacherService.getMCQPaperByCode(papercode);
                //return Json(data);
                if (data.TeacherEmailId != User.Identity.Name)
                {
                    ViewBag.ErrorTitle = "Invaid Access !";
                    ViewBag.Message = "You are not author of this paper , so you can not see this mcq paper which contains answer also.";
                    return View("Error");
                }
                return View("PaperDetails", data);
            }
            else
            {
                var paperdto = TeacherService.getDescriptivePaper(papercode);
                return View("DescriptivePaperDetails", paperdto);
            }
        }
    }
}