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
    public class TransaccionesController : ControllerBase
    {
        private readonly ITransaccionesService _transaccionesService;

        public TransaccionesController(ITransaccionesService transaccionesService)
        {
            _transaccionesService = transaccionesService;
        }

        // Registrar un depósito (POST)
        [HttpPost("depositar")]
        public async Task<IActionResult> Depositar(int idCuenta, decimal monto)
        {
            //try
            //{
                var exito = await _transaccionesService.RealizarDeposito(idCuenta, monto);

                if (exito)
                {
                    return Ok(new { Mensaje = "Depósito realizado con éxito" });
                }
                else
                {
                    return BadRequest(new { Mensaje = "Error al realizar el depósito" });
                }
           // }
           // catch (Exception ex)
           // {
             //   return StatusCode(500, new { Mensaje = "Error inesperado: " + ex.Message });
            //}
        }
    }
}