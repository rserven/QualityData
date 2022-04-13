using System.Linq.Expressions;
using QualityData.Api.Services;
using QualityData.Library.Models;

namespace QualityData.Api.Interfaces;

public interface IClientesService
{
    Task<List<ObtenerCliente>> ObtenerClientes(ClientesService.TipoConsulta tipoConsulta);
    Task<Cliente?> ObtenerCliente(int clienteId);
    Task Guardar(Library.Dto.Cliente cliente);
    Task Eliminar(int clienteId);
}