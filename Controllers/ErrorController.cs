using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExamPortal.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var exp = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exp.Path;
            ViewBag.Message = exp.Error.Message;
            ViewBag.StackTrace = exp.Error.StackTrace;
            return View("Error");
        }
        [Route("/Error/404")]
        public IActionResult PageNotFound()
        {
            ViewBag.ErrorMsg = "sorry ! requested page not found..";
            return View("NotFound");
        }
    }
}