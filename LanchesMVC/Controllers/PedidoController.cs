using LanchesMVC.Models;
using LanchesMVC.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMVC.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        //Método Get não é necessário a implementação
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        //Método POST é necessário a implementação
        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            return View();
        }
    }
}
