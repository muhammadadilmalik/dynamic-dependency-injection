using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Models;
using TestApplication.Services;

namespace TestApplication.Controllers
{
	public class HomeController : Controller
	{
		private ITestingService _testingService;
		private ITestingServiceSingleton _testingServiceSingleton;
		private IScopedService _scopedService;
		public HomeController(ITestingService testingService,
			ITestingServiceSingleton testingServiceSingleton,
			IScopedService scopedService)
		{
			_testingService = testingService;
			_testingServiceSingleton = testingServiceSingleton;
			_scopedService = scopedService;
		}
		public IActionResult Index()
		{
			ViewData["message"] = _testingService.getMessage();
			ViewData["messageSingleton"] = _testingServiceSingleton.getMessage();
			ViewData["messageScoped"] = _scopedService.getMessage();
			ViewData["messageScoped1"] = _scopedService.getMessage();
			ViewData["messageScoped2"] = _scopedService.getMessage();
			ViewData["messageScoped3"] = _scopedService.getMessage();
			ViewData["messageScoped4"] = _scopedService.getMessage();
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
