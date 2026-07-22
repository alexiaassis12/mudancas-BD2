using Mudanca.DTOs;
using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using System.Data;
using MySqlConnector;
using Dapper;

namespace Mudanca.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _connectionString;

        public PedidoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

        public async Task<int> CreateAsync(PedidoCreateDto pedidoDto)
        {
            using var conn = CreateConnection();

            var query = @"INSERT INTO pedido (id_empresa, codigo_cliente, id_cidade_origem, endereco_origem, id_cidade_destino, endereco_destino, data_solicitacao, preco_total, situacao) 
                          VALUES (@IdEmpresa, @CodigoCliente, @IdCidadeOrigem, @EnderecoOrigem, @IdCidadeDestino, @EnderecoDestino, @DataSolicitacao, 0, 'PENDENTE');
                          SELECT LAST_INSERT_ID();";

            return await conn.ExecuteScalarAsync<int>(query, pedidoDto);
        }

        public async Task<bool> AddServicoAsync(PedidoServicoCreateDto pedidoServicoDto)
        {
            using var conn = CreateConnection();

            var query = @"INSERT INTO pedido_servico (codigo_pedido, nome_servico, tempo_duracao, peso_carga) 
                          VALUES (@CodigoPedido, @NomeServico, @TempoDuracao, @PesoCarga);";

            var rowsAffected = await conn.ExecuteAsync(query, pedidoServicoDto);

            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            using var conn = CreateConnection();

            var mysql = @"SELECT codigo_pedido AS CodigoPedido, 
                                 id_empresa AS IdEmpresa, 
                                 codigo_cliente AS CodigoCliente, 
                                 id_cidade_origem AS IdCidadeOrigem, 
                                 endereco_origem AS EnderecoOrigem, 
                                 id_cidade_destino AS IdCidadeDestino, 
                                 endereco_destino AS EnderecoDestino, 
                                 data_solicitacao AS DataSolicitacao, 
                                 preco_total AS PrecoTotal, 
                                 situacao AS Situacao 
                          FROM pedido";

            return await conn.QueryAsync<Pedido>(mysql);
        }

       public async Task<Pedido?> GetByIdAsync(int id)
        {
            using var conn = CreateConnection();

            var mysql = @"SELECT codigo_pedido AS CodigoPedido, 
                                 id_empresa AS IdEmpresa, 
                                 codigo_cliente AS CodigoCliente, 
                                 id_cidade_origem AS IdCidadeOrigem, 
                                 endereco_origem AS EnderecoOrigem, 
                                 id_cidade_destino AS IdCidadeDestino, 
                                 endereco_destino AS EnderecoDestino, 
                                 data_solicitacao AS DataSolicitacao, 
                                 preco_total AS PrecoTotal, 
                                 situacao AS Situacao 
                          FROM pedido 
                          WHERE codigo_pedido = @Id";

            return await conn.QueryFirstOrDefaultAsync<Pedido>(mysql, new { Id = id });
        }
        public async Task<bool> UpdateSituacaoAsync(int codigoPedido, string novaSituacao)
        {
            using var conn = CreateConnection();
            var query = @"UPDATE pedido 
                  SET situacao = @NovaSituacao 
                  WHERE codigo_pedido = @CodigoPedido";

            var rowsAffected = await conn.ExecuteAsync(query, new { CodigoPedido = codigoPedido, NovaSituacao = novaSituacao });
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int codigoPedido)
        {
            using var conn = CreateConnection();
            var query = "DELETE FROM pedido WHERE codigo_pedido = @CodigoPedido";
            var rowsAffected = await conn.ExecuteAsync(query, new { CodigoPedido = codigoPedido });
            return rowsAffected > 0;
        }
    }
}