using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QualityData.Api.Contexts;
using QualityData.Api.Interfaces;
using QualityData.Library.Models;

namespace QualityData.Api.Services
{
    public class ClientesService : IClientesService
    {
        private readonly QualityDataDbContext _context;

        public ClientesService(QualityDataDbContext context)
        {
            _context = context;
        }

        public async Task<List<ObtenerCliente>> ObtenerClientes(TipoConsulta tipoConsulta)
        {
            List<ObtenerCliente> data = new List<ObtenerCliente>();

            switch (tipoConsulta)
            {
                case TipoConsulta.Vista:
                    data = await _context.ObtenerClientes.AsNoTracking().ToListAsync();
                    break;
                case TipoConsulta.Sp:
                    data = await _context.ObtenerClientes.FromSqlRaw("exec [dbo].[ObtenerTodosClientes] @clienteId=0").AsNoTracking().ToListAsync();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipoConsulta), tipoConsulta, null);
            }

            return data;
        }

        public async Task<Cliente?> ObtenerCliente(int clienteId)
        {
            var data = await _context.Clientes.FirstOrDefaultAsync(d => d.ClienteId == clienteId);
            return data;
        }

        public async Task Guardar(Library.Dto.Cliente cliente)
        {
            if (cliente.ClienteId == 0)
            {
                var nuevoCliente = new Cliente
                {
                    Apellido = cliente.Apellido,
                    Documento = cliente.Documento,
                    Nombre = cliente.Nombre,
                    ClienteUbicacions = new List<ClienteUbicacion>() { new() { UbicacionId = 1 } } //Por efecto de evaluacion se coloca un valor por defecto
                };

                await _context.Clientes.AddAsync(nuevoCliente);
            }
            else
            {
                var data = await ObtenerCliente(cliente.ClienteId);
                if (data != null)
                {
                    data.Apellido = cliente.Apellido;
                    data.Documento = cliente.Documento;
                    data.Nombre = cliente.Nombre;
                }
                else
                {
                    throw new Exception($"Cliente no encontrado. Id: {cliente.ClienteId}");
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task Eliminar(int clienteId)
        {
            var data = await ObtenerCliente(clienteId);
            if (data != null)
            {
                var detalle = await _context.ClienteUbicacions.FirstAsync(d => d.ClienteId == clienteId);
                _context.Remove(detalle);
                _context.Remove(data);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Cliente no encontrado. Id: {clienteId}");
            }
        }


        public enum TipoConsulta
        {
            Vista = 0, Sp
        }
    }
}
