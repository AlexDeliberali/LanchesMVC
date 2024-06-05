using Microsoft.AspNetCore.Mvc;

namespace LanchesMVC.Controllers
{
    public class LancheController : Controller
    {


        public IActionResult List()
        {
            return View();
        }
    }
}
