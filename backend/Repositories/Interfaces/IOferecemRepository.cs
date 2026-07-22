using Mudanca.Models;

namespace Mudanca.Repositories.Interfaces
{
    public interface IOferecemRepository
    {
        Task<IEnumerable<Oferecem>> GetAllAsync();
        Task<Oferecem?> GetByKeyAsync(int idEmpresa, int idCidade, string nomeServico);
        Task<bool> CreateAsync(Oferecem oferecem);
    }
}
