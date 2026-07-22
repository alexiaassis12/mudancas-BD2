using Mudanca.DTOs;

namespace Mudanca.Services.Interfaces
{
    public interface IRelatorioService
    {
        Task<IEnumerable<CidadeServicoHistogramaDto>> GetHistogramaServicosPorCidadeAsync();
        Task<IEnumerable<CidadeFaturamentoDto>> GetPagamentosPorCidadeAsync();
        Task<IEnumerable<CidadeFaturamentoDto>> GetTop5CidadesMaiorValorInvestidoAsync();
        Task<IEnumerable<CidadeServicoTotalDto>> GetTop5CidadesNumeroServicosAsync();
        Task<IEnumerable<EmpresaRankingDto>> GetTop5EmpresasPorServicosAsync();
        Task<IEnumerable<EmpresaRankingDto>> GetTop5EmpresasPorValoresGanhosAsync();
    }
}