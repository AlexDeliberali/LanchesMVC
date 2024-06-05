using LanchesMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMVC.Controllers
{
    public class LancheController : Controller
    {

        private readonly ILancheRepository _lancheRepository;
        public LancheController(ILancheRepository lancheRepository) { 
            _lancheRepository = lancheRepository;
        }

        public IActionResult List()
        {
            //Obtendo uma lista de lanches
            var lanches = _lancheRepository.Lanches;
            return View(lanches);
        }
    }
}
