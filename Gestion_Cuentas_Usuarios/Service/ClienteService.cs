using Gestion_Cuentas_Usuarios.DTO;
using Gestion_Cuentas_Usuarios.Entidades;
using Gestion_Cuentas_Usuarios.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Gestion_Cuentas_Usuarios.Service
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _dbContext;

        public ClienteService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ClienteDto>> GetAllClientes()
        {
            var clientes = await _dbContext.Clientes
                .Select(c => new ClienteDto
                {
                    ID = c.ID,
                    NOMBRE = c.NOMBRE,
                    APELLIDO = c.APELLIDO,
                    ESTADO = c.ESTADO
                })
                .ToListAsync();

            return clientes;
        }
    }
}

