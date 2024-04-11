using Gestion_Cuentas_Usuarios.DTO;
using Gestion_Cuentas_Usuarios.Service.Interface;
using System;
using System.Threading.Tasks;

namespace Gestion_Cuentas_Usuarios.Service
{
    public class TransaccionesService : ITransaccionesService
    {
        private readonly ICuentaService _cuentaService;

        public TransaccionesService(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        public async Task<TransaccionesDto> RealizarDeposito(int cuentaId, decimal monto)
        {
            // Llama al método en el servicio de cuentas para realizar la transacción
            return await _cuentaService.RealizarTransaccion(cuentaId, monto);
        }
    }
}