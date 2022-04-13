using System.Linq.Expressions;
using QualityData.Api.Models;
using QualityData.Api.Services;

namespace QualityData.Api.Interfaces;

public interface IClientesService
{
    Task<List<ObtenerCliente>> ObtenerClientes(ClientesService.TipoConsulta tipoConsulta);
    Task<Cliente?> ObtenerCliente(int clienteId);
    Task Guardar(Dto.Cliente cliente);
    Task Eliminar(int clienteId);
}