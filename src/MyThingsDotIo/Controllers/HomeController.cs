using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyThingsDotIo.Models;
using Microsoft.Extensions.Logging;

namespace MyThingsDotIo.Controllers
{
    public class HomeController : Controller
    {
        private IMyThingsDotIoRepository _repository;
        private ILogger<HomeController> _logger;

        public HomeController(IMyThingsDotIoRepository repository, ILogger<HomeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "My personal thethings.iO clone.";

            return View();
        }

        public IActionResult Contact()
        {
            var data = _repository.GetAll();

            return View(data);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
