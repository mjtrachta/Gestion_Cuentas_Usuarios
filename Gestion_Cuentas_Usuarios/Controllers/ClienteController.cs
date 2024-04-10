using Gestion_Cuentas_Usuarios.DTO;
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

        //Listar 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetAllClientes()
        {
            var clientes = await _clienteService.GetAllClientes();
            return Ok(clientes);
        }
    }
}