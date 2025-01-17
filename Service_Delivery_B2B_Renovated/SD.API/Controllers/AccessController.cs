using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SD.Application.Admin.Acceso.DTOs;
using SD.Application.Interfaces.Services.Admin.Acceso;
using System.Security.Claims;

namespace SD.API.Controllers
{
	public class AccessController : Controller
	{
		private readonly IAccesoService _accesoService;

		public AccessController(IAccesoService accesoService)
		{
			_accesoService = accesoService;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Index(CreateLoginDTO login)
		{
			try
			{
				var result = await _accesoService.Login(login);

				if (result == null)
				{
					ViewBag.Error = "No se han encontrado coincidencias";
					return View();
				}

				AuthenticationProperties properties = new AuthenticationProperties()
				{
					AllowRefresh = true,
					IsPersistent = true
				};

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(result.claims),
					properties
					);

				return RedirectToAction("Index", "Home");
			}
			catch(Exception ex) 
			{
                ViewBag.Error = ex.Message;
				return View();
			}
		}
	}
}
