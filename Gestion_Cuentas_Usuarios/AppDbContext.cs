using Gestion_Cuentas_Usuarios.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Gestion_Cuentas_Usuarios
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<TipoTransaccion> Tipo_Transaccion { get; set; }
        public DbSet<Transacciones> Transacciones { get; set; }
       

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

    }
}
