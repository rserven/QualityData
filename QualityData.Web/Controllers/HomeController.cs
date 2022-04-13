using Microsoft.AspNetCore.Mvc;
using QualityData.Web.Models;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using QualityData.Library.Models;

namespace QualityData.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private const string httpClienteAddress = "https://localhost:7151";


        #region HttpCliente

        private async Task<List<Cliente>?> ObtenerClientes()
        {
            var clientes = new List<Cliente>();

            using var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.GetAsync(httpClienteAddress + "/Clientes/ObtenerPorVista");

            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            var clientesData = JsonConvert.DeserializeObject<List<ObtenerCliente>>(content);

            foreach (var clienteId in clientesData.Select(d => d.ClienteId).Distinct())
            {
                var currentCliente = clientesData.FirstOrDefault(d => d.ClienteId == clienteId);

                clientes.Add(new Cliente
                {
                    Apellido = currentCliente.Apellido,
                    ClienteId = currentCliente.ClienteId,
                    Documento = currentCliente.Documento,
                    Nombre = currentCliente.Nombre
                });
            }

            return clientes;
        }

        private async Task<Cliente?> ObtenerCliente(int id)
        {
            using var httpClient = new HttpClient();
            var httpResponseMessage = await httpClient.GetAsync(httpClienteAddress + $"/Clientes/ObtenerCliente/{id}");

            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content)) return null;
            var cliente = JsonConvert.DeserializeObject<Cliente>(content);
            if (cliente != null)
            {
                return cliente;
            }

            return null;
        }

        private async Task Guardar(Library.Dto.Cliente cliente)
        {
            using var httpClient = new HttpClient();

            JsonContent clienteContent = JsonContent.Create(cliente);

            var httpResponseMessage = await httpClient.PostAsync(httpClienteAddress + "/Clientes/ObtenerPorVista/Guardar", clienteContent);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"No se pudo guardar la informacion {httpResponseMessage.Content}");
            }

        }



        #endregion


        public async Task<IActionResult> Index()
        {
            var clientes = await ObtenerClientes();

            return View(clientes);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await ObtenerCliente((int)id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Library.Dto.Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await Guardar(cliente);

                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await ObtenerCliente((int)id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Library.Dto.Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await Guardar(cliente);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ClienteListExists(cliente.ClienteId))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }


        // GET: EngineerCheckLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await ObtenerCliente((int)id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: EngineerCheckLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using var httpClient = new HttpClient();

            var httpResponseMessage = await httpClient.DeleteAsync(httpClienteAddress + $"/Clientes/ObtenerPorVista/Eliminar{id}");

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception($"No se pudo guardar la informacion {httpResponseMessage.Content}");
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ClienteListExists(int id)
        {
            return (await ObtenerClientes() ?? new List<Cliente>()).Any(e => e.ClienteId == id);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}