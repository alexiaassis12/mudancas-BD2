using Mudanca.DTOs;
using Mudanca.Models;

namespace Mudanca.Services.Interfaces
{
    public interface IServicoService
    {
        Task<IEnumerable<Servico>> GetAllAsync();
        Task<Servico?> GetByIdAsync(string nomeServico);
        Task<string> CreateAsync(ServicoCreateDto servicoDto);
        Task<bool> UpdateAsync(string nomeServico, ServicoCreateDto servicoDto);
        Task<bool> DeleteAsync(string nomeServico);
    }
}
