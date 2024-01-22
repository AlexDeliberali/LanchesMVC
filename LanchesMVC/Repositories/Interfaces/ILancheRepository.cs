using LanchesMVC.Models;

namespace LanchesMVC.Repositories.Interfaces
{
    public interface ILancheRepository
    {
        IEnumerable<Lanche> Lanches { get; }
        IEnumerable<Lanche> LanchesPreferidos { get; }
        Lanche GetLancheByID(int lancheID);
    }
}
