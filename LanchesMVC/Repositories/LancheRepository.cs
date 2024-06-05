using LanchesMVC.Context;
using LanchesMVC.Models;
using LanchesMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesMVC.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;
        public LancheRepository(AppDbContext contexto)
        {
            _context = contexto;
        }
        IEnumerable<Lanche> ILancheRepository.Lanches => _context.Lanches.Include(c => c.Categoria);

        IEnumerable<Lanche> ILancheRepository.LanchesPreferidos => _context.Lanches.Where(l => l.IsLanchePreferido).Include(c => c.Categoria);

        Lanche ILancheRepository.GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        }
    }
}
