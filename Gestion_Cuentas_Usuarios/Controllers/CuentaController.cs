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

        // Listar todas las cuentas 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaDto>>> GetAllCuentas()
        {
            var cuentas = await _cuentaService.GetAllCuentas();
            return Ok(cuentas);
        }

        // Listar cuentas por cliente 
        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<CuentaDto>>> GetCuentasByCliente(int clienteId)
        {
            var cuentas = await _cuentaService.GetCuentasByCliente(clienteId);
            return Ok(cuentas);
        }

        // Crear cuenta para un cliente 
        [HttpPost("{clienteId}")]
        public async Task<ActionResult<CuentaDto>> CreateCuenta(int clienteId)
        {
            var nuevaCuenta = await _cuentaService.CreateCuenta(clienteId);
            return Ok(nuevaCuenta);
        }

        // Crear la transaccion deposito y sumar el monto a la cuenta
        [HttpPost("{cuentaId}/deposito")]
        public async Task<ActionResult<TransaccionesDto>> RealizarTransaccion(int cuentaId, [FromBody] decimal monto)
        {
            var transaccion = await _cuentaService.RealizarTransaccion(cuentaId, monto);

            if (transaccion == null)
            {
                return BadRequest("No se pudo realizar la transacción. Verifique la cuenta y su estado.");
            }

            return Ok(transaccion);
        }

        // Crear la transaccion transferencia, restar el monto a la cuenta origen y sumar el monto a la cuenta destino
        [HttpPost("transferencia")]
        public async Task<ActionResult> RealizarTransferencia(int cuentaOrigenId, int cuentaDestinoId, decimal monto)
        {
            var resultado = await _cuentaService.RealizarTransferencia(cuentaOrigenId, cuentaDestinoId, monto);

            if (!resultado)
            {
                return BadRequest("No se pudo realizar la transferencia. Verifique las cuentas y los saldos.");
            }

            return Ok("Transferencia realizada con éxito.");
        }
        // Buscar todas las transacciones de una cuenta
        [HttpGet("{cuentaId}/transacciones")]
        public async Task<ActionResult<IEnumerable<TransaccionesDto>>> GetTransaccionesByCuenta(int cuentaId)
        {
            var transacciones = await _cuentaService.GetTransaccionesByCuenta(cuentaId);
            return Ok(transacciones);
        }

        // sumar el saldo total para un cliente dado
        [HttpGet("cliente/{clienteId}/saldo")]
        public async Task<ActionResult<decimal>> GetSaldoCliente(int clienteId)
        {
            var saldoCliente = await _cuentaService.CalcularSaldoCliente(clienteId);
            return Ok(saldoCliente);
        }
    }
}