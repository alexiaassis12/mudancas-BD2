using Mudanca.DTOs;
using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using System.Data;
using MySqlConnector;
using Dapper;

namespace Mudanca.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

        public async Task<int> CreateAsync(ClienteCreateDto clienteDto)
        {
            using var conn = CreateConnection();

            var query = @"INSERT INTO cliente (cpf, rg, nome_completo, endereco) 
                          VALUES (@Cpf, @Rg, @NomeCompleto, @Endereco);
                          SELECT LAST_INSERT_ID();";

            return await conn.ExecuteScalarAsync<int>(query, clienteDto);
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            using var conn = CreateConnection();

            var mysql = @"SELECT codigo_cliente AS CodigoCliente, 
                                 cpf AS Cpf, 
                                 rg AS Rg, 
                                 nome_completo AS NomeCompleto, 
                                 endereco AS Endereco 
                          FROM cliente";

            return await conn.QueryAsync<Cliente>(mysql);
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            using var conn = CreateConnection();

            var mysql = @"SELECT codigo_cliente AS CodigoCliente, 
                                 cpf AS Cpf, 
                                 rg AS Rg, 
                                 nome_completo AS NomeCompleto, 
                                 endereco AS Endereco 
                          FROM cliente 
                          WHERE codigo_cliente = @Id";

            return await conn.QueryFirstOrDefaultAsync<Cliente>(mysql, new { Id = id });
        }

        public async Task<bool> UpdateAsync(int id, ClienteCreateDto clienteDto)
        {
            using var conn = CreateConnection();
            var query = @"UPDATE cliente 
                  SET cpf = @Cpf, rg = @Rg, nome_completo = @NomeCompleto, endereco = @Endereco 
                  WHERE codigo_cliente = @Id";

            var rowsAffected = await conn.ExecuteAsync(query, new
            {
                Id = id,
                clienteDto.Cpf,
                clienteDto.Rg,
                clienteDto.NomeCompleto,
                clienteDto.Endereco
            });
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var conn = CreateConnection();
            var query = "DELETE FROM cliente WHERE codigo_cliente = @Id";
            var rowsAffected = await conn.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }
    }
}