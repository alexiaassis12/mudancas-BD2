using Mudanca.DTOs;
using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using Mudanca.Services.Interfaces;

namespace Mudanca.Services
{
    public class FuncionarioService : IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;   
        }
        public async Task<string> CreateAsync(FuncionarioCreateDto funcionarioDto)
        {
            ValidarFuncionarioDto(funcionarioDto);

            if (string.IsNullOrWhiteSpace(funcionarioDto.CpfFunc))
                throw new ArgumentException("O CPF do funcionário é obrigatório no cadastro.");

            return await _funcionarioRepository.CreateAsync(funcionarioDto);
        }

        public async Task<bool> DeleteAsync(string cpf)
        {
            await GetByCpfAsync(cpf);
            return await _funcionarioRepository.DeleteAsync(cpf);
        }

        public async Task<IEnumerable<Funcionario>> GetAllAsync()
        {
            return await _funcionarioRepository.GetAllAsync();
        }

        public async Task<Funcionario?> GetByCpfAsync(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("O CPF do funcionário não pode ser vazio.");

            var funcionario = await _funcionarioRepository.GetByCpfAsync(cpf);
            if (funcionario == null)
                throw new KeyNotFoundException($"Funcionário com CPF {cpf} não foi encontrado.");

            return funcionario;
        }

        public async Task<bool> UpdateAsync(string cpf, FuncionarioCreateDto funcionarioDto)
        {
            await GetByCpfAsync(cpf);
            ValidarFuncionarioDto(funcionarioDto);
            return await _funcionarioRepository.UpdateAsync(cpf, funcionarioDto);
        }

        private void ValidarFuncionarioDto(FuncionarioCreateDto funcionarioDto)
        {
            if (string.IsNullOrWhiteSpace(funcionarioDto.NomeCompleto))
                throw new ArgumentException("O nome completo do funcionário é obrigatório.");

            if (string.IsNullOrWhiteSpace(funcionarioDto.TipoFunc))
                throw new ArgumentException("O tipo/cargo do funcionário é obrigatório.");

            if (funcionarioDto.Salario < 0)
                throw new ArgumentException("O salário do funcionário não pode ser negativo.");
        }
    }
}
