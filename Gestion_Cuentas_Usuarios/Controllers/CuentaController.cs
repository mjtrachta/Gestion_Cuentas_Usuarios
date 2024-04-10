using Gestion_Cuentas_Usuarios.DTO;
using Gestion_Cuentas_Usuarios.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Cuentas_Usuarios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;

        public CuentaController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        // Listar todas las cuentas (GET)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaDto>>> GetAllCuentas()
        {
            var cuentas = await _cuentaService.GetAllCuentas();
            return Ok(cuentas);
        }

        // Listar cuentas por cliente (GET)
        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<CuentaDto>>> GetCuentasByCliente(int clienteId)
        {
            var cuentas = await _cuentaService.GetCuentasByCliente(clienteId);
            return Ok(cuentas);
        }

        // Crear cuenta para un cliente (POST)
        [HttpPost("{clienteId}")]
        public async Task<ActionResult<CuentaDto>> CreateCuenta(int clienteId)
        {
            var nuevaCuenta = await _cuentaService.CreateCuenta(clienteId);
            return Ok(nuevaCuenta);
        }
    }
}