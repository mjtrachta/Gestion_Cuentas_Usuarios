using System.ComponentModel.DataAnnotations;

namespace Gestion_Cuentas_Usuarios.Entidades
{
    public class TipoTransaccion
    {
        [Key]
        public int ID { get; set; }
        public string NOMBRE  { get; set; }
    }
}
