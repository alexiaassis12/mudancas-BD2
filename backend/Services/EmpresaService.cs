using Mudanca.DTOs;
using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using Mudanca.Services.Interfaces;

namespace Mudanca.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }
        public async Task<int> CreateAsync(EmpresaCreateDto empresaDto)
        {
            ValidarEmpresaDto(empresaDto);
            return await _empresaRepository.CreateAsync(empresaDto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await GetByIdAsync(id);

            return await _empresaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            return await _empresaRepository.GetAllAsync();
        }

        public async Task<Empresa?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("O ID da empresa deve ser um número inteiro positivo.");
            }

            var empresa = await _empresaRepository.GetByIdAsync(id);

            if (empresa == null)
            {
                throw new KeyNotFoundException($"Empresa com o ID {id} não foi encontrada.");
            }

            return empresa;
        }

        public async Task<bool> UpdateAsync(int id, EmpresaCreateDto empresaDto)
        {
            await GetByIdAsync(id);
            ValidarEmpresaDto(empresaDto);
            return await _empresaRepository.UpdateAsync(id, empresaDto);
        }

        private void ValidarEmpresaDto(EmpresaCreateDto empresaDto)
        {
            if (string.IsNullOrWhiteSpace(empresaDto.Nome))
            {
                throw new ArgumentException("O nome da empresa obrigatório.");
            }
            if (string.IsNullOrEmpty(empresaDto.Endereco))
            {
                throw new ArgumentException("O endereço da empresa obrigatório.");
            }
        }
    }
}
