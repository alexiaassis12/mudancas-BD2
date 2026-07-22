using Mudanca.DTOs;
using Mudanca.Models;

namespace Mudanca.Services.Interfaces
{
    public interface ICidadeService
    {
        Task<IEnumerable<Cidade>> GetAllAsync();
        Task<Cidade?> GetByIdAsync(int id);
        Task<int> CreateAsync(CidadeCreateDto cidadeDto);
        Task<bool> UpdateAsync(int id, CidadeCreateDto cidadeDto);
        Task<bool> DeleteAsync(int id);
    }
}
