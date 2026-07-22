using Mudanca.DTOs;
using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using Mudanca.Services.Interfaces;

namespace Mudanca.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }
        public async Task<int> CreateAsync(CidadeCreateDto cidadeDto)
        {
            if (string.IsNullOrWhiteSpace(cidadeDto.NomeCidade))
            {
                throw new ArgumentException("O nome da cidade não pode estar vazio.");
            }

            if (string.IsNullOrWhiteSpace(cidadeDto.Estado) || cidadeDto.Estado.Length != 2)
            {
                throw new ArgumentException("O estado (UF) deve ter exatamente 2 letras (ex: SP, RJ).");
            }

            cidadeDto.Estado = cidadeDto.Estado.ToUpper();

            return await _cidadeRepository.CreateAsync(cidadeDto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await GetByIdAsync(id);

            return await _cidadeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Cidade>> GetAllAsync()
        {
            return await _cidadeRepository.GetAllAsync();
        }

        public async Task<Cidade?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("O ID da cidade deve ser um número inteiro positivo.");
            }

            var cidade = await _cidadeRepository.GetByIdAsync(id);

            if (cidade == null)
            {
                throw new KeyNotFoundException($"Cidade com o ID {id} não foi encontrada.");
            }

            return cidade;
        }

        public async Task<bool> UpdateAsync(int id, CidadeCreateDto cidadeDto)
        {
            await GetByIdAsync(id);

            if (string.IsNullOrWhiteSpace(cidadeDto.NomeCidade))
            {
                throw new ArgumentException("O nome da cidade não pode estar vazio.");
            }

            cidadeDto.Estado = cidadeDto.Estado.ToUpper();

            return await _cidadeRepository.UpdateAsync(id, cidadeDto);
        }
    }
}
