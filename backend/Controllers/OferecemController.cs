using Microsoft.AspNetCore.Mvc;
using Mudanca.Models;
using Mudanca.Services.Interfaces;

namespace Mudanca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OferecemController : ControllerBase
    {
        private readonly IOferecemService _oferecemService;

        public OferecemController(IOferecemService oferecemService)
        {
            _oferecemService = oferecemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var catalogo = await _oferecemService.GetAllAsync();
            return Ok(catalogo);
        }

        [HttpGet("{idEmpresa:int}/{idCidade:int}/{nomeServico}")]
        public async Task<IActionResult> GetByKey(int idEmpresa, int idCidade, string nomeServico)
        {
            try
            {
                var item = await _oferecemService.GetByKeyAsync(idEmpresa, idCidade, nomeServico);
                return Ok(item);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Oferecem oferecem)
        {
            try
            {
                await _oferecemService.CreateAsync(oferecem);
                return Ok(new { mensagem = "Preço do serviço cadastrado com sucesso!" });
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
    }
}