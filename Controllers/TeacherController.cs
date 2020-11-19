using ExamPortal.DTOS;
using ExamPortal.Services;
using ExamPortal.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ExamPortal.Controllers
{
    public class TeacherController : Controller
    {
        #region Constructor and Properties
        public TeacherController(ITeacherService teacherService) => TeacherService = teacherService;

        public ITeacherService TeacherService { get; }
        #endregion



        public IActionResult Index(int? pages)
        {
            var data = TeacherService.getAllPapersByEmailId(User.Identity.Name, pages ?? 1);
            ViewBag.currentpage = pages ?? 1;
            ViewBag.total = data.Item1;
            return View(data.Item2);
        }
        [HttpGet]
        public IActionResult MakePaper() => View();
        [HttpPost]
        public IActionResult MakePaper(MCQPaperDTO paper)
        {
            if (ModelState.IsValid)
            {
                //Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

                paper.TeacherEmailId = User.Identity.Name;
                TeacherService.CreateMCQPaper(paper);
                return RedirectToAction(nameof(Index), "teacher");
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
            return RedirectToAction(nameof(Index));
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
        [HttpGet]
        public IActionResult Responses(string papercode)
        {
            if (CodeGenerator.GetPaperType(papercode) == EPaperType.MCQ)
            {
                var answersheet = TeacherService.GetAnswerSheetsBycode(papercode);
                return View(answersheet);
            }
            else
            {
                var answersheet = TeacherService.GetDescriptiveAnswerSheetsBycode(papercode);
                return View("ResponsesOfDescriptive", answersheet);
            }
        }
        [HttpPost]
        public IActionResult GetEnterMarks(string AnswerSheet, string Paper)
        {
            var answersheet = JsonConvert.DeserializeObject<DescriptiveAnswerSheetDTO>(AnswerSheet);
            answersheet.Paper = JsonConvert.DeserializeObject<DescriptivePaperDTO>(Paper);
            return View("EnterMarks", answersheet);
        }
        [HttpPost]
        public IActionResult PostEnterMarks(string papercode, int marksgiven, string studentname)
        {
            TeacherService.SetMarksInDescriptivePaper(papercode, marksgiven, studentname);
            return RedirectToAction("Responses", new { papercode = papercode });
        }
    }
}