using Mudanca.DTOs;
using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using System.Data;
using MySqlConnector;
using Dapper;

namespace Mudanca.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly string _connectionString;

        public ServicoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

        public async Task<string> CreateAsync(ServicoCreateDto servicoDto)
        {
            using var conn = CreateConnection();

            var query = @"INSERT INTO servico (nome_servico, tipo_especializacao) 
                          VALUES (@NomeServico, @TipoEspecializacao);";

            await conn.ExecuteAsync(query, servicoDto);

            return servicoDto.NomeServico;
        }

        public async Task<IEnumerable<Servico>> GetAllAsync()
        {
            using var conn = CreateConnection();

            var mysql = @"SELECT nome_servico AS NomeServico, 
                                 tipo_especializacao AS TipoEspecializacao 
                          FROM servico";

            return await conn.QueryAsync<Servico>(mysql);
        }

        public async Task<Servico?> GetByIdAsync(string nomeServico)
        {
            using var conn = CreateConnection();

            var mysql = @"SELECT nome_servico AS NomeServico, 
                                 tipo_especializacao AS TipoEspecializacao 
                          FROM servico 
                          WHERE nome_servico = @NomeServico";

            return await conn.QueryFirstOrDefaultAsync<Servico>(mysql, new { NomeServico = nomeServico });
        }

        public async Task<bool> UpdateAsync(string nomeServico, ServicoCreateDto servicoDto)
        {
            using var conn = CreateConnection();
            var query = @"UPDATE servico 
                  SET tipo_specializacao = @TipoEspecializacao 
                  WHERE nome_servico = @NomeServico";

            var rowsAffected = await conn.ExecuteAsync(query, new { NomeServico = nomeServico, servicoDto.TipoEspecializacao });
            return rowsAffected > 0;
        }
        public async Task<bool> DeleteAsync(string nomeServico)
        {
            using var conn = CreateConnection();
            var query = "DELETE FROM servico WHERE nome_servico = @NomeServico";
            var rowsAffected = await conn.ExecuteAsync(query, new { NomeServico = nomeServico });
            return rowsAffected > 0;
        }
    }
}