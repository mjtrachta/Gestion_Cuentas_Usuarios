using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestion_Cuentas_Usuarios.Entidades
{
    public class Transacciones
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ID_TRANSACCION { get; set; }
        public DateTime FECHA_HORA { get; set; }
        public decimal MONTO { get; set; }
        public int ID_CUENTA { get; set; }
        public int ID_TIPO_MOVIMIENTO { get; set; }
    }
}
