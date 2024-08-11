using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    //tem que estar autenticado e pertencer ao perfil de admin
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
