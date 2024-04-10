using Gestion_Cuentas_Usuarios.DTO;
using Gestion_Cuentas_Usuarios.Entidades;
using Gestion_Cuentas_Usuarios.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Cuentas_Usuarios.Service
{
    public class CuentaService : ICuentaService
    {
        private readonly AppDbContext _dbContext;

        public CuentaService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CuentaDto>> GetAllCuentas()
        {
            var cuentas = await _dbContext.Cuentas
                .Select(c => new CuentaDto
                {
                    ID = c.ID,
                    ID_CLIENTE = c.ID_CLIENTE,
                    SALDO = c.SALDO,
                    ESTADO = c.ESTADO
                })
                .ToListAsync();

            return cuentas;
        }

        public async Task<IEnumerable<CuentaDto>> GetCuentasByCliente(int clienteId)
        {
            var cuentas = await _dbContext.Cuentas
                .Where(c => c.ID_CLIENTE == clienteId)
                .Select(c => new CuentaDto
                {
                    ID = c.ID,
                    ID_CLIENTE = c.ID_CLIENTE,
                    SALDO = c.SALDO,
                    ESTADO = c.ESTADO
                })
                .ToListAsync();

            return cuentas;
        }

        public async Task<CuentaDto> CreateCuenta(int clienteId)
        {
            var nuevaCuenta = new Cuenta
            {
                ID_CLIENTE = clienteId,
                SALDO = 0, // saldo inicial cero
                ESTADO = 1 // estado activo
            };

            _dbContext.Cuentas.Add(nuevaCuenta);
            await _dbContext.SaveChangesAsync();

            return new CuentaDto
            {
                ID = nuevaCuenta.ID,
                ID_CLIENTE = nuevaCuenta.ID_CLIENTE,
                SALDO = nuevaCuenta.SALDO,
                ESTADO = nuevaCuenta.ESTADO
            };
        }
    }
}