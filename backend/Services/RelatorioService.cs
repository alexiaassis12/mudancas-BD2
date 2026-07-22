using Mudanca.DTOs;
using Mudanca.Repositories.Interfaces;
using Mudanca.Services.Interfaces;

namespace Mudanca.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioService(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public async Task<IEnumerable<CidadeServicoHistogramaDto>> GetHistogramaServicosPorCidadeAsync()
        {
            return await _relatorioRepository.GetHistogramaServicosPorCidadeAsync();
        }

        public async Task<IEnumerable<CidadeFaturamentoDto>> GetPagamentosPorCidadeAsync()
        {
            return await _relatorioRepository.GetPagamentosPorCidadeAsync();
        }

        public async Task<IEnumerable<CidadeFaturamentoDto>> GetTop5CidadesMaiorValorInvestidoAsync()
        {
            return await _relatorioRepository.GetTop5CidadesMaiorValorInvestidoAsync();
        }

        public async Task<IEnumerable<CidadeServicoTotalDto>> GetTop5CidadesNumeroServicosAsync()
        {
            return await _relatorioRepository.GetTop5CidadesNumeroServicosAsync();
        }

        public async Task<IEnumerable<EmpresaRankingDto>> GetTop5EmpresasPorServicosAsync()
        {
            return await _relatorioRepository.GetTop5EmpresasPorServicosAsync();
        }

        public async Task<IEnumerable<EmpresaRankingDto>> GetTop5EmpresasPorValoresGanhosAsync()
        {
            return await _relatorioRepository.GetTop5EmpresasPorValoresGanhosAsync();
        }
    }
}