using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementCore.Controllers
{
    public class ErrorController : Controller
    {
		[AllowAnonymous]
		//[Route("Error/{statusCode}")]
		public IActionResult HttpStatusCodehandler(int statusCode)
		{
			switch (statusCode)
			{
				case 404:
					ViewBag.ErrorMessage = "Sorry, The resource you requested could not be found";
					break;
				default:
					break;
			}

			return View("NotFound");
		}
	}
}
