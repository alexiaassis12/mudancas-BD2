using Mudanca.DTOs;
using Mudanca.Models;

namespace Mudanca.Services.Interfaces
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<Funcionario>> GetAllAsync();
        Task<Funcionario?> GetByCpfAsync(string cpf);
        Task<string> CreateAsync(FuncionarioCreateDto funcionarioDto);
        Task<bool> UpdateAsync(string cpf, FuncionarioCreateDto funcionarioDto);
        Task<bool> DeleteAsync(string cpf);
    }
}
