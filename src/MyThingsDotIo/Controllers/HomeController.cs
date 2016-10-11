using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyThingsDotIo.Models;

namespace MyThingsDotIo.Controllers
{
    public class HomeController : Controller
    {
        private IMyThingsDotIoRepository _repository;

        public HomeController(IMyThingsDotIoRepository repository)
        {
            _repository = repository;
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
            var data = _repository.GetAllUsers();

            return View(data);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
