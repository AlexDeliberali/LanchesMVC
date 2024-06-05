using LanchesMVC.Models;

namespace LanchesMVC.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        //Propriedade somente de leitura
        IEnumerable<Categoria> Categorias { get; }
    }
}
