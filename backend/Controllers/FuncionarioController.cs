using Microsoft.AspNetCore.Mvc;
using Mudanca.DTOs;
using Mudanca.Services.Interfaces;

namespace Mudanca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var funcionarios = await _funcionarioService.GetAllAsync();
            return Ok(funcionarios);
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            try
            {
                var funcionario = await _funcionarioService.GetByCpfAsync(cpf);
                return Ok(funcionario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FuncionarioCreateDto funcionarioDto)
        {
            try
            {
                var cpf = await _funcionarioService.CreateAsync(funcionarioDto);
                return CreatedAtAction(nameof(GetByCpf), new { cpf }, funcionarioDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{cpf}")]
        public async Task<IActionResult> Update(string cpf, [FromBody] FuncionarioCreateDto funcionarioDto)
        {
            try
            {
                await _funcionarioService.UpdateAsync(cpf, funcionarioDto);
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

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> Delete(string cpf)
        {
            try
            {
                await _funcionarioService.DeleteAsync(cpf);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}