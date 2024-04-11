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

        // 1. Listar Cliente
        public async Task<IEnumerable<Cliente>> GetAllClientes()
        {
            var clientes = await _dbContext.Clientes
                .Select(c => new Cliente
                {
                    ID = c.ID,
                    NOMBRE = c.NOMBRE,
                    APELLIDO = c.APELLIDO,
                    ESTADO = c.ESTADO
                })
                .ToListAsync();

            return clientes;
        }
        // 2. Crear Cliente

        public async Task<Cliente> CreateCliente(Cliente cliente)
        {
            var nuevoCliente = new Cliente
            {
                NOMBRE = cliente.NOMBRE,
                APELLIDO = cliente.APELLIDO,
                ESTADO = cliente.ESTADO
            };

            _dbContext.Clientes.Add(nuevoCliente);
            await _dbContext.SaveChangesAsync();

            cliente.ID = nuevoCliente.ID;
            return cliente;
        }

        // 3. Editar Cliente
        public async Task<ClienteDto> UpdateCliente(int id, ClienteDto clienteDto)
        {
            var clienteExistente = await _dbContext.Clientes.FindAsync(id);

            if (clienteExistente == null)
                throw new ArgumentException("Cliente no encontrado");

            clienteExistente.NOMBRE = clienteDto.NOMBRE;
            clienteExistente.APELLIDO = clienteDto.APELLIDO;

            await _dbContext.SaveChangesAsync();

            return clienteDto;
        }

        // 4. Borrado (Logico) Cliente
        public async Task<bool> DeleteCliente(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);
            if (cliente == null)
                throw new ArgumentException("Cliente no encontrado");

            // Verificar el saldo de las cuentas asociadas al cliente
            var cuentasConSaldo = await _dbContext.Cuentas.AnyAsync(c => c.ID_CLIENTE == id && c.SALDO > 0);
            if (cuentasConSaldo)
                return false; // No se puede eliminar el cliente si tiene cuentas con saldo positivo

            // Cambiar el estado del cliente a 0 (borrado lógico)
            cliente.ESTADO = 0;

            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}

