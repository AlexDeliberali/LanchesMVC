using LanchesMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMVC.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _catRepository;

        public CategoriaMenu(ICategoriaRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _catRepository.Categorias.OrderBy(c => c.CategoriaNome);
            return View(categorias);
        }
    }
}
