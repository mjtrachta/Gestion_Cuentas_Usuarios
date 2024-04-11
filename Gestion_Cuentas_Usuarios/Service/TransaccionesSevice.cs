using Gestion_Cuentas_Usuarios.DTO;
using Gestion_Cuentas_Usuarios.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Cuentas_Usuarios.Service
{
    public class TransaccionesService : ITransaccionesService
    {
        private readonly AppDbContext _dbContext;

        public TransaccionesService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TransaccionesDto>> GetTransaccionesByTipo(int tipoMovimientoId)
        {
            var transacciones = await (from t in _dbContext.Transacciones
                                       join tt in _dbContext.Tipo_Transaccion on t.ID_TIPO_MOVIMIENTO equals tt.ID
                                       where t.ID_TIPO_MOVIMIENTO == tipoMovimientoId
                                       select new TransaccionesDto
                                       {
                                           ID_TRANSACCION = t.ID_TRANSACCION,
                                           FECHA_HORA = t.FECHA_HORA,
                                           MONTO = t.MONTO,
                                           ID_CUENTA = t.ID_CUENTA,
                                           ID_TIPO_MOVIMIENTO = t.ID_TIPO_MOVIMIENTO,
                                           NOMBRE_TIPO_MOVIMIENTO = tt.NOMBRE
                                       }).ToListAsync();

            return transacciones;
        }
    }
}