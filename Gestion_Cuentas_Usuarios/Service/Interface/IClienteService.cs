using Gestion_Cuentas_Usuarios.DTO;
using Gestion_Cuentas_Usuarios.Entidades;
using System.Threading.Tasks;

namespace Gestion_Cuentas_Usuarios.Service.Interface
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> GetAllClientes();
        Task<Cliente> CreateCliente(Cliente cliente);
        Task<ClienteDto> UpdateCliente(int id, ClienteDto clienteDto);
        Task<bool> DeleteCliente(int id);
    }
}
