using System.ComponentModel.DataAnnotations;

namespace Gestion_Cuentas_Usuarios.Entidades
{
    public class Cuenta

    {
        [Key]
        public int ID { get; set; }
        public int ID_CLIENTE { get; set; }
        public decimal SALDO { get; set; }
        public int ESTADO { get; set; }
    }
}
