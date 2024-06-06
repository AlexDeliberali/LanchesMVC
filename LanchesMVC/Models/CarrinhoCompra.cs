using LanchesMVC.Context;

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
            string carrinhoId = session.GetString("carrinhoId") ?? Guid.NewGuid().ToString();

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
                                        s => s.lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    lanche = lanche,
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
                                        s => s.lanche.LancheId == lanche.LancheId && s.CarrinhoCompraId == CarrinhoCompraId);

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
    }
}
