using Microsoft.AspNetCore.Mvc;
using Gestion_Cuentas_Usuarios.DTO;
using Gestion_Cuentas_Usuarios.Service.Interface;
using System.Threading.Tasks;

namespace Gestion_Cuentas_Usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesController : ControllerBase
    {
        private readonly ITransaccionesService _transaccionesService;

        public TransaccionesController(ITransaccionesService transaccionesService)
        {
            _transaccionesService = transaccionesService;
        }

        // obtener transacciones por tipo
        [HttpGet("tipo/{tipoMovimientoId}")]
        public async Task<ActionResult<IEnumerable<TransaccionesDto>>> GetTransaccionesByTipo(int tipoMovimientoId)
        {
            var transacciones = await _transaccionesService.GetTransaccionesByTipo(tipoMovimientoId);
            if (transacciones == null)
            {
                return NotFound();
            }
            return Ok(transacciones);
        }

    
    }
}