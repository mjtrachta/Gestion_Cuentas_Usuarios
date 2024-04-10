using Gestion_Cuentas_Usuarios.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Cuentas_Usuarios.Service.Interface
{
    public interface ICuentaService
    {
        Task<IEnumerable<CuentaDto>> GetAllCuentas();
        Task<IEnumerable<CuentaDto>> GetCuentasByCliente(int clienteId);
        Task<CuentaDto> CreateCuenta(int clienteId);
    }
}
