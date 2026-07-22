using Mudanca.DTOs;
using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using Mudanca.Services.Interfaces;

namespace Mudanca.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<int> CreateAsync(ClienteCreateDto clienteDto)
        {
            ValidarClienteDto(clienteDto);
            return await _clienteRepository.CreateAsync(clienteDto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await GetByIdAsync(id);
            return await _clienteRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _clienteRepository.GetAllAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("O código do cliente deve ser um número positivo.");
            }

            var cliente = await _clienteRepository.GetByIdAsync(id);

            if(cliente == null)
            {
                throw new KeyNotFoundException($"Cliente com o código {id} não foi encontrado.");
            }

            return cliente;
        }

        public async Task<bool> UpdateAsync(int id, ClienteCreateDto clienteDto)
        {
            await GetByIdAsync(id);
            ValidarClienteDto(clienteDto);
            return await _clienteRepository.UpdateAsync(id, clienteDto);
        }

        private void ValidarClienteDto(ClienteCreateDto clienteDto)
        {
            if (string.IsNullOrWhiteSpace(clienteDto.NomeCompleto))
                throw new ArgumentException("O nome completo do cliente é obrigatório.");

            if (string.IsNullOrWhiteSpace(clienteDto.Cpf))
                throw new ArgumentException("O CPF do cliente é obrigatório.");

            if (string.IsNullOrWhiteSpace(clienteDto.Rg))
                throw new ArgumentException("O RG do cliente é obrigatório.");

            if (string.IsNullOrWhiteSpace(clienteDto.Endereco))
                throw new ArgumentException("O endereço do cliente é obrigatório.");
        }
    }
}
