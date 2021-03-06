using Microsoft.AspNetCore.Mvc;
using QualityData.Api.Interfaces;
using QualityData.Api.Services;
using QualityData.Library.Dto;

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
        public async Task<IActionResult> ObtenerPorVista()
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
        public async Task<IActionResult> ObtenerPorSp()
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

        [HttpGet]
        [Route("ObtenerCliente/{clienteId}")]
        public async Task<IActionResult> ObtenerCliente([FromRoute] int clienteId)
        {
            try
            {
                var data = await _clientesService.ObtenerCliente(clienteId);
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
        public async Task<IActionResult> Guardar(Cliente cliente)
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