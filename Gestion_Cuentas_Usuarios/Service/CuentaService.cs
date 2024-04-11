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

        // Listar todas las cuentas 
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

        // Listar cuentas por cliente 
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

        // Crear cuenta para un cliente
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

        // Realizar Deposito
        public async Task<TransaccionesDto> RealizarTransaccion(int cuentaId, decimal monto)
        {
            var cuenta = await _dbContext.Cuentas.FindAsync(cuentaId);

            if (cuenta == null || cuenta.ESTADO != 1)
            {
                return null; // La cuenta no existe o no está activa
            }

            if (monto <= 0)
            {
                return null; // El monto de la transacción no es válido
            }

            // Crear una nueva transacción
            var transaccion = new Transacciones
            {
                FECHA_HORA = DateTime.Now,
                MONTO = monto,
                ID_CUENTA = cuentaId,
                ID_TIPO_MOVIMIENTO = 1 // tipo de transacción Deposito
            };

            // Actualizar el saldo de la cuenta
            cuenta.SALDO += monto;

            // Guardar la transaccion y actualizar el saldo de la cuenta en la base de datos
            _dbContext.Transacciones.Add(transaccion);
            await _dbContext.SaveChangesAsync();

            // Mapear la transaccion a TransaccionesDto y devolverla
            return new TransaccionesDto
            {
                ID_TRANSACCION = transaccion.ID_TRANSACCION,
                FECHA_HORA = transaccion.FECHA_HORA,
                MONTO = transaccion.MONTO,
                ID_CUENTA = transaccion.ID_CUENTA,
                ID_TIPO_MOVIMIENTO = transaccion.ID_TIPO_MOVIMIENTO
            };
        }

        //Realizar Transferencia
        public async Task<bool> RealizarTransferencia(int cuentaOrigenId, int cuentaDestinoId, decimal monto)
        {

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var cuentaOrigen = await _dbContext.Cuentas.FindAsync(cuentaOrigenId);
                    var cuentaDestino = await _dbContext.Cuentas.FindAsync(cuentaDestinoId);

                    if (cuentaOrigen == null || cuentaDestino == null || cuentaOrigen.ESTADO != 1 || cuentaDestino.ESTADO != 1)
                    {
                        return false; // La cuenta de origen o destino no existe o no está activa
                    }

                    if (monto <= 0 || cuentaOrigen.SALDO < monto)
                    {
                        return false; // El monto de la transferencia no es válido o excede el saldo de la cuenta de origen
                    }

                    // Crear una nueva transacción para la cuenta de origen
                    var transaccionOrigen = new Transacciones
                    {
                        FECHA_HORA = DateTime.Now,
                        MONTO = -monto, // La cantidad se resta de la cuenta de origen
                        ID_CUENTA = cuentaOrigenId,
                        ID_TIPO_MOVIMIENTO = 2 // 2 representa el tipo de transacción "Transferencia saliente"
                    };

                    // Crear una nueva transacción para la cuenta de destino
                    var transaccionDestino = new Transacciones
                    {
                        FECHA_HORA = DateTime.Now,
                        MONTO = monto, // La cantidad se suma a la cuenta de destino
                        ID_CUENTA = cuentaDestinoId,
                        ID_TIPO_MOVIMIENTO = 3 // 3 representa el tipo de transacción "Transferencia entrante"
                    };

                    // Actualizar los saldos de las cuentas
                    cuentaOrigen.SALDO -= monto;
                    cuentaDestino.SALDO += monto;

                    // Guardar las transacciones y actualizar los saldos de las cuentas en la base de datos
                    _dbContext.Transacciones.Add(transaccionOrigen);
                    _dbContext.Transacciones.Add(transaccionDestino);
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync(); // Commit de la transacción

                    return true; // La transferencia se completó con éxito
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(); // Rollback de la transacción en caso de error
                    return false; // La transferencia no se pudo completar
                }
            }
        }

        public async Task<IEnumerable<TransaccionesDto>> GetTransaccionesByCuenta(int cuentaId)
        {
            var transacciones = await _dbContext.Transacciones
                .Where(t => t.ID_CUENTA == cuentaId)
                .Select(t => new TransaccionesDto
                {
                    ID_TRANSACCION = t.ID_TRANSACCION,
                    FECHA_HORA = t.FECHA_HORA,
                    MONTO = t.MONTO,
                    ID_CUENTA = t.ID_CUENTA,
                    ID_TIPO_MOVIMIENTO = t.ID_TIPO_MOVIMIENTO
                })
                .ToListAsync();

            return transacciones;
        }

        public async Task<decimal> CalcularSaldoCliente(int clienteId)
        {
            var cuentas = await _dbContext.Cuentas
                .Where(c => c.ID_CLIENTE == clienteId)
                .ToListAsync();

            decimal saldoTotal = cuentas.Sum(c => c.SALDO);

            return saldoTotal;
        }
    }
}