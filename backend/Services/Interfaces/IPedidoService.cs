using Mudanca.DTOs;
using Mudanca.Models;

namespace Mudanca.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> GetAllAsync();
        Task<Pedido?> GetByIdAsync(int id);
        Task<int> CreateAsync(PedidoCreateDto pedidoDto);
        Task<bool> AddServicoAsync(PedidoServicoCreateDto pedidoServicoDto);
        Task<bool> UpdateSituacaoAsync(int codigoPedido, string novaSituacao);
        Task<bool> DeleteAsync(int id);
    }
}
