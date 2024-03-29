﻿using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private LanguageService _localization;

		public HomeController(ILogger<HomeController> logger, LanguageService localization )
		{
			_logger = logger;
			_localization = localization;
		}

		public IActionResult Index()
		{
			

			ViewBag.WelcomeMessage = _localization.Getkey("str_welcome_message").Value;
			var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
			return View();
		}


		public IActionResult ChangeLanguage(string culture)
		{
			Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

			return Redirect(Request.Headers["Referer"].ToString());
		}


		public IActionResult Register() 
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(RegisterViewModel model)
		{
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