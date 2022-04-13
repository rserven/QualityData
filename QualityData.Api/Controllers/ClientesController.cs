using Microsoft.AspNetCore.Mvc;
using QualityData.Api.Interfaces;
using QualityData.Api.Models;
using QualityData.Api.Services;

namespace QualityData.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesService _clientesService;

        private readonly ILogger<ClientesController> _logger;

        public ClientesController(ILogger<ClientesController> logger, IClientesService clientesService)
        {
            _logger = logger;
            _clientesService = clientesService;
        }

        [HttpGet]
        [Route("ObtenerPorVista")]
        public async Task<IActionResult> ObtenerPorVista(int clienteId)
        {
            try
            {
                var data = await _clientesService.ObtenerClientes(ClientesService.TipoConsulta.Vista);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("ObtenerPorSp")]
        public async Task<IActionResult> ObtenerPorSp(int clienteId)
        {
            try
            {
                var data = await _clientesService.ObtenerClientes(ClientesService.TipoConsulta.Sp);
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(Dto.Cliente cliente)
        {
            try
            {
                await _clientesService.Guardar(cliente);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        public async Task<IActionResult> Eliminar(int clienteId)
        {
            try
            {
                await _clientesService.Eliminar(clienteId);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}