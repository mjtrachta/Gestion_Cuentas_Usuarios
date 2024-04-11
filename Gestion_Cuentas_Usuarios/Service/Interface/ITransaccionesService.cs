using Gestion_Cuentas_Usuarios.DTO;

namespace Gestion_Cuentas_Usuarios.Service.Interface
{
    public interface ITransaccionesService
    {
        Task<TransaccionesDto> RealizarDeposito(int cuentaId, decimal monto);
    }
}
