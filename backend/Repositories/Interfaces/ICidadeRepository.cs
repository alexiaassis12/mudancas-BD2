using Mudanca.DTOs;
using Mudanca.Models;

namespace Mudanca.Repositories.Interfaces
{
    public interface ICidadeRepository
    {
        Task<IEnumerable<Cidade>> GetAllAsync();
        Task<Cidade?> GetByIdAsync(int id);
        Task<int> CreateAsync(CidadeCreateDto cidadeDto);
        Task<bool> UpdateAsync(int id, CidadeCreateDto cidadeDto);
        Task<bool> DeleteAsync(int id);
    }
}
