using Mudanca.DTOs;
using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using System.Data;
using MySqlConnector;
using Dapper;

namespace Mudanca.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private readonly string _connectionString;

        public FuncionarioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

        public async Task<string> CreateAsync(FuncionarioCreateDto funcionarioDto)
        {
            using var conn = CreateConnection();

            var query = @"INSERT INTO funcionario (cpf_func, rg_func, nome_completo, endereco, tipo_func, salario) 
                          VALUES (@CpfFunc, @RgFunc, @NomeCompleto, @Endereco, @TipoFunc, @Salario);";

            await conn.ExecuteAsync(query, funcionarioDto);

            return funcionarioDto.CpfFunc;
        }

        public async Task<IEnumerable<Funcionario>> GetAllAsync()
        {
            using var conn = CreateConnection();

            var mysql = @"SELECT cpf_func AS CpfFunc, 
                                 rg_func AS RgFunc, 
                                 nome_completo AS NomeCompleto, 
                                 endereco AS Endereco, 
                                 tipo_func AS TipoFunc, 
                                 salario AS Salario 
                          FROM funcionario";

            return await conn.QueryAsync<Funcionario>(mysql);
        }

        public async Task<Funcionario?> GetByCpfAsync(string cpf)
        {
            using var conn = CreateConnection();

            var mysql = @"SELECT cpf_func AS CpfFunc, 
                                 rg_func AS RgFunc, 
                                 nome_completo AS NomeCompleto, 
                                 endereco AS Endereco, 
                                 tipo_func AS TipoFunc, 
                                 salario AS Salario 
                          FROM funcionario 
                          WHERE cpf_func = @Cpf";

            return await conn.QueryFirstOrDefaultAsync<Funcionario>(mysql, new { Cpf = cpf });
        }

        public async Task<bool> UpdateAsync(string cpf, FuncionarioCreateDto funcionarioDto)
        {
            using var conn = CreateConnection();
            var query = @"UPDATE funcionario 
                  SET rg_func = @RgFunc, nome_completo = @NomeCompleto, endereco = @Endereco, tipo_func = @TipoFunc, salario = @Salario 
                  WHERE cpf_func = @Cpf";

            var rowsAffected = await conn.ExecuteAsync(query, new
            {
                Cpf = cpf,
                funcionarioDto.RgFunc,
                funcionarioDto.NomeCompleto,
                funcionarioDto.Endereco,
                funcionarioDto.TipoFunc,
                funcionarioDto.Salario
            });
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(string cpf)
        {
            using var conn = CreateConnection();
            var query = "DELETE FROM funcionario WHERE cpf_func = @Cpf";
            var rowsAffected = await conn.ExecuteAsync(query, new { Cpf = cpf });
            return rowsAffected > 0;
        }
    }
}