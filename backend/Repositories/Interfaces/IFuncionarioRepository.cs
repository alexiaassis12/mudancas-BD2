using Mudanca.DTOs;
using Mudanca.Models;

namespace Mudanca.Repositories.Interfaces
{
    public interface IFuncionarioRepository
    {
        Task<IEnumerable<Funcionario>> GetAllAsync();
        Task<Funcionario?> GetByCpfAsync(string cpf);
        Task<string> CreateAsync(FuncionarioCreateDto funcionarioDto);
        Task<bool> UpdateAsync(string cpf, FuncionarioCreateDto funcionarioDto);
        Task<bool> DeleteAsync(string cpf);
    }
}
