using Mudanca.Models;
using Mudanca.DTOs;

namespace Mudanca.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task<int> CreateAsync(ClienteCreateDto clienteDto);
        Task<bool> UpdateAsync(int id, ClienteCreateDto clienteDto);
        Task<bool> DeleteAsync(int id);
    }
}
