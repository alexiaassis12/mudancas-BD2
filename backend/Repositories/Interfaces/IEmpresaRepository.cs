using Mudanca.DTOs;
using Mudanca.Models;

namespace Mudanca.Repositories.Interfaces
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> GetAllAsync();
        Task<Empresa?> GetByIdAsync(int id);
        Task<int> CreateAsync(EmpresaCreateDto empresaDto);
        Task<bool> UpdateAsync(int id, EmpresaCreateDto empresaDto);
        Task<bool> DeleteAsync(int id);
    }
}
