using Gestion_Cuentas_Usuarios.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Cuentas_Usuarios.Service.Interface

{
    public interface IClienteService
    {
        // Todos los clientes
        Task<IEnumerable<ClienteDto>> GetAllClientes();
    }
}
