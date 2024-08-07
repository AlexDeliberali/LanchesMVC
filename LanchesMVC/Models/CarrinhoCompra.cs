﻿using LanchesMVC.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMVC.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        //Colocando o contexto dentro do construtor
        public CarrinhoCompra(AppDbContext contexto)
        {
                _context = contexto;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem>CarrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            // Definindo uma sessão
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //Obtendo um serviço do tipo do nosso contexto
            var context = services.GetService<AppDbContext>();

            //Obtem um existente ou gera um ID do carrinho
            // ?? = faz o mesmo que o NVL/Coalesce do banco, se um for nulo, pega o outro
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //Atribuindo o ID do carrinho na Sessão aberta
            session.SetString("CarrinhoId", carrinhoId);

            //Retornando o carrinho com o contexto e o ID atruibuido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            //SingleOrDefault = Retorna somente um único elemento que atenda aos requisitos informados
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                                        s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };
                //Inserindo no banco de dados
                _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }

            _context.SaveChanges();
        }

        public int RemoverDoCarrinho(Lanche lanche)
        {
            //SingleOrDefault = Retorna somente um único elemento que atenda aos requisitos informados
            var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
                                        s => s.Lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if (carrinhoCompraItem != null)
            {
                if(carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }
            _context.SaveChanges();
            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem>GetCarrinhoCompraItems()
        {
            return CarrinhoCompraItems ?? (CarrinhoCompraItems = _context.CarrinhoCompraItens
                                                                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                                                                .Include(s => s.Lanche)
                                                                .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                                .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItens
                        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                        .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

            return total;
        }
    }
}