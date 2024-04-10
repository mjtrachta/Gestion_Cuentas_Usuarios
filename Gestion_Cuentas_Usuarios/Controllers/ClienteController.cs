using Gestion_Cuentas_Usuarios.DTO;
using Gestion_Cuentas_Usuarios.Entidades;
using Gestion_Cuentas_Usuarios.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Cuentas_Usuarios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // 1. Listar clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAllClientes()
        {
            var clientes = await _clienteService.GetAllClientes();
            return Ok(clientes);
        }

        // 2. Crear cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
        {
            var nuevoCliente = await _clienteService.CreateCliente(cliente);
            return Ok(nuevoCliente);
        }

        // 3. Editar cliente
        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDto>> UpdateCliente(int id, ClienteDto clienteDto)
        {
            var clienteActualizado = await _clienteService.UpdateCliente(id, clienteDto);
            return Ok(clienteActualizado);
        }

        // Borrar cliente 
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCliente(int id)
        {
            var success = await _clienteService.DeleteCliente(id);
            if (success)
                return Ok(true);
            else
                return BadRequest("No se pudo borrar el cliente debido a cuentas con saldo positivo.");
        }
    }
}