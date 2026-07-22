using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using System.Data;
using MySqlConnector;
using Dapper;

namespace Mudanca.Repositories
{
    public class OferecemRepository : IOferecemRepository
    {
        private readonly string _connectionString;

        public OferecemRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new MySqlConnection(_connectionString);

        public async Task<bool> CreateAsync(Oferecem oferecem)
        {
            using var conn = CreateConnection();

            var query = @"INSERT INTO oferecem (id_empresa, id_cidade, nome_servico, preco_hora) 
                          VALUES (@IdEmpresa, @IdCidade, @NomeServico, @PrecoHora);";

            var rowsAffected = await conn.ExecuteAsync(query, oferecem);

            return rowsAffected > 0;
        }

        public async Task<IEnumerable<Oferecem>> GetAllAsync()
        {
            using var conn = CreateConnection();

            var mysql = @"SELECT id_empresa AS IdEmpresa, 
                                 id_cidade AS IdCidade, 
                                 nome_servico AS NomeServico, 
                                 preco_hora AS PrecoHora 
                          FROM oferecem";

            return await conn.QueryAsync<Oferecem>(mysql);
        }

        public async Task<Oferecem?> GetByKeyAsync(int idEmpresa, int idCidade, string nomeServico)
        {
            using var conn = CreateConnection();

            var mysql = @"SELECT id_empresa AS IdEmpresa, 
                                 id_cidade AS IdCidade, 
                                 nome_servico AS NomeServico, 
                                 preco_hora AS PrecoHora 
                          FROM oferecem 
                          WHERE id_empresa = @IdEmpresa 
                            AND id_cidade = @IdCidade 
                            AND nome_servico = @NomeServico";

            return await conn.QueryFirstOrDefaultAsync<Oferecem>(mysql, new
            {
                IdEmpresa = idEmpresa,
                IdCidade = idCidade,
                NomeServico = nomeServico
            });
        }
    }
}