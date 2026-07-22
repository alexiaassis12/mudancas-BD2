using Microsoft.AspNetCore.Mvc;
using Mudanca.DTOs;
using Mudanca.Services.Interfaces;

namespace Mudanca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : ControllerBase
    {
        private readonly IServicoService _servicoService;

        public ServicoController(IServicoService servicoService)
        {
            _servicoService = servicoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var servicos = await _servicoService.GetAllAsync();
            return Ok(servicos);
        }

        [HttpGet("{nomeServico}")]
        public async Task<IActionResult> GetById(string nomeServico)
        {
            try
            {
                var servico = await _servicoService.GetByIdAsync(nomeServico);
                return Ok(servico);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServicoCreateDto servicoDto)
        {
            try
            {
                var nome = await _servicoService.CreateAsync(servicoDto);
                return CreatedAtAction(nameof(GetById), new { nomeServico = nome }, servicoDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{nomeServico}")]
        public async Task<IActionResult> Update(string nomeServico, [FromBody] ServicoCreateDto servicoDto)
        {
            try
            {
                await _servicoService.UpdateAsync(nomeServico, servicoDto);
                return NoContent();
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

        [HttpDelete("{nomeServico}")]
        public async Task<IActionResult> Delete(string nomeServico)
        {
            try
            {
                await _servicoService.DeleteAsync(nomeServico);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}