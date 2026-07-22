using Mudanca.Models;

namespace Mudanca.Services.Interfaces
{
    public interface IOferecemService
    {
        Task<IEnumerable<Oferecem>> GetAllAsync();
        Task<Oferecem?> GetByKeyAsync(int idEmpresa, int idCidade, string nomeServico);
        Task<bool> CreateAsync(Oferecem oferecem);
    }
}
