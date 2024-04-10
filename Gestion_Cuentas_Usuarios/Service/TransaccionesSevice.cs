using System;
using Gestion_Cuentas_Usuarios;
using Gestion_Cuentas_Usuarios.Entidades;
using Gestion_Cuentas_Usuarios.Service.Interface;
using Microsoft.EntityFrameworkCore;

public class TransaccionesService : ITransaccionesService
{
    private readonly AppDbContext _dbContext;

    public TransaccionesService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> RealizarDeposito(int idCuenta, decimal monto)
    {
        // Validar que la cuenta exista y esté activa
        var cuenta = await _dbContext.Cuentas.FindAsync(idCuenta);
        if (cuenta == null || cuenta.ESTADO != 1)
        {
            return false;
        }

        // Registrar la transacción
        var transaccion = new Transacciones
        {
            ID_TIPO_MOVIMIENTO = 1, // Tipo de movimiento: Depósito
            MONTO = monto,
            ID_CUENTA = idCuenta,
            FECHA_HORA = DateTime.Now
        };

        _dbContext.Transacciones.Add(transaccion);

        // Actualizar el saldo de la cuenta
        cuenta.SALDO += monto;

        // Guardar los cambios
        await _dbContext.SaveChangesAsync();

        return true;
    }
}