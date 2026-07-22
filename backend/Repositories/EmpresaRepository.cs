using Mudanca.DTOs;
using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using System.Data;
using MySqlConnector;
using Dapper;

namespace Mudanca.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly string _connectionString;

        public EmpresaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

        public async Task<int> CreateAsync(EmpresaCreateDto empresaDto)
        {
            using var conn = CreateConnection();

            var query = @"INSERT INTO empresa (nome, endereco) 
                          VALUES (@Nome, @Endereco);
                          SELECT LAST_INSERT_ID();";

            return await conn.ExecuteScalarAsync<int>(query, empresaDto);
        }


        public async Task<IEnumerable<Empresa>> GetAllAsync()
        {
            using var conn = CreateConnection();

            var mysql = "SELECT id_empresa AS IdEmpresa, nome AS Nome, endereco AS Endereco FROM empresa";

            return await conn.QueryAsync<Empresa>(mysql);
        }

        public async Task<Empresa?> GetByIdAsync(int id)
        {
            using var conn = CreateConnection();

            var mysql = "SELECT id_empresa AS IdEmpresa, nome AS Nome, endereco AS Endereco FROM empresa WHERE id_empresa = @Id";

            return await conn.QueryFirstOrDefaultAsync<Empresa>(mysql, new { Id = id });
        }
        public async Task<bool> UpdateAsync(int id, EmpresaCreateDto empresaDto)
        {
            using var conn = CreateConnection();
            var query = @"UPDATE empresa 
                  SET nome = @Nome, endereco = @Endereco 
                  WHERE id_empresa = @Id";

            var rowsAffected = await conn.ExecuteAsync(query, new { Id = id, empresaDto.Nome, empresaDto.Endereco });
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = CreateConnection();
            var query = "DELETE FROM empresa WHERE id_empresa = @Id";
            var rowsAffected = await conn.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
    
}