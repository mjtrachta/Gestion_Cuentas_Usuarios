using System.ComponentModel.DataAnnotations;

namespace Gestion_Cuentas_Usuarios.Entidades
{
    public class Cuenta

    {
        [Key]
        public int ID { get; set; }
        public int ID_CLIENTE { get; set; }
        public decimal SALDO { get; set; }
        // EL ESTADO 0 ES BAJA, EL 1 ES ACTIVO
        public int ESTADO { get; set; }
    }
}
