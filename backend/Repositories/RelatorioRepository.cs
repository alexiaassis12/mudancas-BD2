using System.Data;
using Dapper;
using MySqlConnector;
using Mudanca.DTOs;
using Mudanca.Repositories.Interfaces;

namespace Mudanca.Repositories
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly string _connectionString;

        public RelatorioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string não encontrada no appsettings.json.");
        }

        private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

        public async Task<IEnumerable<CidadeServicoHistogramaDto>> GetHistogramaServicosPorCidadeAsync()
        {
            using var conn = CreateConnection();
            string query = @"SELECT 
                                NomeCidade, 
                                NomeServico, 
                                QuantidadeServicos 
                             FROM view_histograma_servicos_cidade;";

            return await conn.QueryAsync<CidadeServicoHistogramaDto>(query);
        }

        public async Task<IEnumerable<CidadeFaturamentoDto>> GetPagamentosPorCidadeAsync()
        {
            using var conn = CreateConnection();
            string query = @"SELECT 
                                IdCidade, 
                                NomeCidade, 
                                Estado, 
                                ValorInvestido 
                             FROM view_faturamento_cidade;";

            return await conn.QueryAsync<CidadeFaturamentoDto>(query);
        }

        public async Task<IEnumerable<CidadeFaturamentoDto>> GetTop5CidadesMaiorValorInvestidoAsync()
        {
            using var conn = CreateConnection();
            string query = @"SELECT 
                                IdCidade, 
                                NomeCidade, 
                                Estado, 
                                ValorInvestido 
                             FROM view_faturamento_cidade 
                             ORDER BY ValorInvestido DESC 
                             LIMIT 5;";

            return await conn.QueryAsync<CidadeFaturamentoDto>(query);
        }

        public async Task<IEnumerable<CidadeServicoTotalDto>> GetTop5CidadesNumeroServicosAsync()
        {
            using var conn = CreateConnection();
            string query = @"SELECT 
                                IdCidade, 
                                NomeCidade, 
                                Estado, 
                                TotalServicos 
                             FROM view_servicos_cidade 
                             ORDER BY TotalServicos DESC 
                             LIMIT 5;";

            return await conn.QueryAsync<CidadeServicoTotalDto>(query);
        }

        public async Task<IEnumerable<EmpresaRankingDto>> GetTop5EmpresasPorServicosAsync()
        {
            using var conn = CreateConnection();
            string query = @"SELECT 
                                IdEmpresa, 
                                NomeEmpresa, 
                                TotalServicosSolicitados, 
                                ValoresGanhos 
                             FROM view_ranking_empresas 
                             ORDER BY TotalServicosSolicitados DESC 
                             LIMIT 5;";

            return await conn.QueryAsync<EmpresaRankingDto>(query);
        }

        public async Task<IEnumerable<EmpresaRankingDto>> GetTop5EmpresasPorValoresGanhosAsync()
        {
            using var conn = CreateConnection();
            string query = @"SELECT 
                                IdEmpresa, 
                                NomeEmpresa, 
                                TotalServicosSolicitados, 
                                ValoresGanhos 
                             FROM view_ranking_empresas 
                             ORDER BY ValoresGanhos DESC 
                             LIMIT 5;";

            return await conn.QueryAsync<EmpresaRankingDto>(query);
        }
    }
}