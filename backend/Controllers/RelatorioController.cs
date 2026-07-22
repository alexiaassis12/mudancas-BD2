using Microsoft.AspNetCore.Mvc;
using Mudanca.DTOs;
using Mudanca.Services.Interfaces;

namespace Mudanca.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatorioController(IRelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet("histograma-servicos-cidade")]
        public async Task<ActionResult<IEnumerable<CidadeServicoHistogramaDto>>> GetHistogramaServicosPorCidade()
        {
            var resultado = await _relatorioService.GetHistogramaServicosPorCidadeAsync();
            return Ok(resultado);
        }

        [HttpGet("pagamentos-cidade")]
        public async Task<ActionResult<IEnumerable<CidadeFaturamentoDto>>> GetPagamentosPorCidade()
        {
            var resultado = await _relatorioService.GetPagamentosPorCidadeAsync();
            return Ok(resultado);
        }

        [HttpGet("top5-cidades-valor-investido")]
        public async Task<ActionResult<IEnumerable<CidadeFaturamentoDto>>> GetTop5CidadesMaiorValorInvestido()
        {
            var resultado = await _relatorioService.GetTop5CidadesMaiorValorInvestidoAsync();
            return Ok(resultado);
        }

        [HttpGet("top5-cidades-numero-servicos")]
        public async Task<ActionResult<IEnumerable<CidadeServicoTotalDto>>> GetTop5CidadesNumeroServicos()
        {
            var resultado = await _relatorioService.GetTop5CidadesNumeroServicosAsync();
            return Ok(resultado);
        }

        [HttpGet("top5-empresas-servicos")]
        public async Task<ActionResult<IEnumerable<EmpresaRankingDto>>> GetTop5EmpresasPorServicos()
        {
            var resultado = await _relatorioService.GetTop5EmpresasPorServicosAsync();
            return Ok(resultado);
        }

        [HttpGet("top5-empresas-valores-ganhos")]
        public async Task<ActionResult<IEnumerable<EmpresaRankingDto>>> GetTop5EmpresasPorValoresGanhos()
        {
            var resultado = await _relatorioService.GetTop5EmpresasPorValoresGanhosAsync();
            return Ok(resultado);
        }
    }
}