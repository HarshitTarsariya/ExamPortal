using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ExamPortal.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExamPortal.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (TempData["Message"] != null)
                ViewBag.Data = TempData["Message"].ToString();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
