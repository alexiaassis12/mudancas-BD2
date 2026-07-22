using Mudanca.DTOs;
using Mudanca.Models;
using Mudanca.Repositories.Interfaces;
using Mudanca.Services.Interfaces;

namespace Mudanca.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IServicoRepository _servicoRepository;

        public PedidoService(
            IPedidoRepository pedidoRepository,
            IClienteRepository clienteRepository,
            IEmpresaRepository empresaRepository,
            ICidadeRepository cidadeRepository,
            IServicoRepository servicoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _clienteRepository = clienteRepository;
            _empresaRepository = empresaRepository;
            _cidadeRepository = cidadeRepository;
            _servicoRepository = servicoRepository;
        }

        public async Task<bool> AddServicoAsync(PedidoServicoCreateDto pedidoServicoDto)
        {
            var pedido = await GetByIdAsync(pedidoServicoDto.CodigoPedido);

            if (pedido!.Situacao.ToUpper() == "CONCLUIDO" || pedido.Situacao.ToUpper() == "CANCELADO")
            {
                throw new InvalidOperationException($"Não é possível adicionar serviços a um pedido {pedido.Situacao}.");
            }

            var servico = await _servicoRepository.GetByIdAsync(pedidoServicoDto.NomeServico);
            if (servico == null)
                throw new KeyNotFoundException($"Serviço '{pedidoServicoDto.NomeServico}' não cadastrado.");

            if (pedidoServicoDto.TempoDuracao <= 0)
                throw new ArgumentException("O tempo de duração do serviço deve ser maior que zero.");

            if (pedidoServicoDto.PesoCarga < 0)
                throw new ArgumentException("O peso da carga não pode ser negativo.");

            return await _pedidoRepository.AddServicoAsync(pedidoServicoDto);
        }

        public async Task<int> CreateAsync(PedidoCreateDto pedidoDto)
        {
            var cliente = await _clienteRepository.GetByIdAsync(pedidoDto.CodigoCliente);
            if (cliente == null)
                throw new KeyNotFoundException($"Cliente código {pedidoDto.CodigoCliente} não existe.");

            var empresa = await _empresaRepository.GetByIdAsync(pedidoDto.IdEmpresa);
            if (empresa == null)
                throw new KeyNotFoundException($"Empresa ID {pedidoDto.IdEmpresa} não existe.");

            var cidadeOrigem = await _cidadeRepository.GetByIdAsync(pedidoDto.IdCidadeOrigem ?? 0);
            if (cidadeOrigem == null)
                throw new KeyNotFoundException($"Cidade de origem ID {pedidoDto.IdCidadeOrigem} não existe.");

            var cidadeDestino = await _cidadeRepository.GetByIdAsync(pedidoDto.IdCidadeDestino ?? 0);
            if (cidadeDestino == null)
                throw new KeyNotFoundException($"Cidade de destino ID {pedidoDto.IdCidadeDestino} não existe.");

            if (pedidoDto.DataSolicitacao == default)
            {
                pedidoDto.DataSolicitacao = DateTime.Now;
            }

            return await _pedidoRepository.CreateAsync(pedidoDto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await GetByIdAsync(id);
            return await _pedidoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Pedido>> GetAllAsync()
        {
            return await _pedidoRepository.GetAllAsync();
        }

        public async Task<Pedido?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("O código do pedido deve ser um número positivo.");

            var pedido = await _pedidoRepository.GetByIdAsync(id);
            if (pedido == null)
                throw new KeyNotFoundException($"Pedido com código {id} não foi encontrado.");

            return pedido;
        }

        public async Task<bool> UpdateSituacaoAsync(int codigoPedido, string novaSituacao)
        {
            await GetByIdAsync(codigoPedido);

            string statusUpper = novaSituacao.ToUpper().Trim();

            var statusPermitidos = new[] { "PENDENTE", "EM_ANDAMENTO", "CONCLUIDO", "CANCELADO" };

            if (!statusPermitidos.Contains(statusUpper))
            {
                throw new ArgumentException($"Situação inválida. Use um dos valores: {string.Join(", ", statusPermitidos)}.");
            }

            return await _pedidoRepository.UpdateSituacaoAsync(codigoPedido, statusUpper);
        }
    }
}
