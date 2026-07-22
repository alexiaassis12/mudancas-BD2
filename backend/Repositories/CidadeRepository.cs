using Mudanca.DTOs;
using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using System.Data;
using MySqlConnector;
using Dapper;

namespace Mudanca.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly string _connectionString;
        public CidadeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
        
        public async Task<int> CreateAsync(CidadeCreateDto cidadeDto)
        {
            using var conn = CreateConnection();

            var query = @"INSERT INTO cidade (nome_cidade, estado) 
                          VALUES (@NomeCidade, @Estado);
                          SELECT LAST_INSERT_ID();";

            return await conn.ExecuteScalarAsync<int>(query, cidadeDto);
        }

        public async Task<IEnumerable<Cidade>> GetAllAsync()
        {
            using var conn = CreateConnection();
            var mysql = "SELECT id_cidade AS IdCidade, nome_cidade AS NomeCidade, estado AS Estado FROM cidade";
            return await conn.QueryAsync<Cidade>(mysql);
        }

        public async Task<Cidade?> GetByIdAsync(int id)
        {
            using var conn = CreateConnection();
            var mysql = "SELECT id_cidade AS IdCidade, nome_cidade AS NomeCidade, estado AS Estado FROM cidade WHERE id_cidade = @Id";
            return await conn.QueryFirstOrDefaultAsync<Cidade>(mysql, new { Id = id });
        }

        public async Task<bool> UpdateAsync(int id, CidadeCreateDto cidadeDto)
        {
            using var conn = CreateConnection();
            var query = @"UPDATE cidade 
                  SET nome_cidade = @NomeCidade, estado = @Estado 
                  WHERE id_cidade = @Id";

            var rowsAffected = await conn.ExecuteAsync(query, new { Id = id, cidadeDto.NomeCidade, cidadeDto.Estado });
            return rowsAffected > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = CreateConnection();
            var query = "DELETE FROM cidade WHERE id_cidade = @Id";
            var rowsAffected = await conn.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
    
}
