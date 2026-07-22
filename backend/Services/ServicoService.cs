using Mudanca.DTOs;
using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using Mudanca.Services.Interfaces;

namespace Mudanca.Services
{
    public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _servicoRepository;

        public ServicoService(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }
        public async Task<string> CreateAsync(ServicoCreateDto servicoDto)
        {
            if (string.IsNullOrWhiteSpace(servicoDto.NomeServico))
                throw new ArgumentException("O nome do serviço é obrigatório.");

            if (string.IsNullOrWhiteSpace(servicoDto.TipoEspecializacao))
                throw new ArgumentException(" O tipo de especialização é obrigatório.");

            return await _servicoRepository.CreateAsync(servicoDto);
        }

        public async Task<bool> DeleteAsync(string nomeServico)
        {
            await GetByIdAsync(nomeServico);
            return await _servicoRepository.DeleteAsync(nomeServico);
        }

        public async Task<IEnumerable<Servico>> GetAllAsync()
        {
            return await _servicoRepository.GetAllAsync();
        }

        public async Task<Servico?> GetByIdAsync(string nomeServico)
        {
            if (string.IsNullOrWhiteSpace(nomeServico))
                throw new ArgumentException("O nome do serviço é obrigatório.");

            var servico = await _servicoRepository.GetByIdAsync(nomeServico);
            if (servico == null)
                throw new KeyNotFoundException($"Serviço '{nomeServico}' não foi encontrado.");

            return servico;
        }

        public async Task<bool> UpdateAsync(string nomeServico, ServicoCreateDto servicoDto)
        {
            await GetByIdAsync(nomeServico);

            if (string.IsNullOrWhiteSpace(servicoDto.TipoEspecializacao))
                throw new ArgumentException("O tipo de especialização é obrigatório.");

            return await _servicoRepository.UpdateAsync(nomeServico, servicoDto); throw new NotImplementedException();
        }
    }
}
