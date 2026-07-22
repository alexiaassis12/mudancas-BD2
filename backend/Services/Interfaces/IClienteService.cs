using Mudanca.DTOs;
using Mudanca.Models;

namespace Mudanca.Services.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task<int> CreateAsync(ClienteCreateDto clienteDto);
        Task<bool> UpdateAsync(int id, ClienteCreateDto clienteDto);
        Task<bool> DeleteAsync(int id);
    }
}
