using Microsoft.AspNetCore.Mvc;
using Mudanca.DTOs;
using Mudanca.Services.Interfaces;

namespace Mudanca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pedidos = await _pedidoService.GetAllAsync();
            return Ok(pedidos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var pedido = await _pedidoService.GetByIdAsync(id);
                return Ok(pedido);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PedidoCreateDto pedidoDto)
        {
            try
            {
                var id = await _pedidoService.CreateAsync(pedidoDto);
                return CreatedAtAction(nameof(GetById), new { id }, new { id, mensagem = "Pedido de mudança aberto com sucesso!" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPost("add-servico")]
        public async Task<IActionResult> AddServico([FromBody] PedidoServicoCreateDto pedidoServicoDto)
        {
            try
            {
                await _pedidoService.AddServicoAsync(pedidoServicoDto);
                return Ok(new { mensagem = "Serviço adicionado ao pedido com sucesso!" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPatch("{id:int}/situacao")]
        public async Task<IActionResult> UpdateSituacao(int id, [FromBody] string novaSituacao)
        {
            try
            {
                await _pedidoService.UpdateSituacaoAsync(id, novaSituacao);
                return Ok(new { mensagem = $"Situação do pedido {id} atualizada para '{novaSituacao}'." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _pedidoService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}