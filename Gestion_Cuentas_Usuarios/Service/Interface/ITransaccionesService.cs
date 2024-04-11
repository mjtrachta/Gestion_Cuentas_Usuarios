using Gestion_Cuentas_Usuarios.DTO;

namespace Gestion_Cuentas_Usuarios.Service.Interface
{
    public interface ITransaccionesService
    {
        Task<IEnumerable<TransaccionesDto>> GetTransaccionesByTipo(int tipoMovimientoId);
    }
}
