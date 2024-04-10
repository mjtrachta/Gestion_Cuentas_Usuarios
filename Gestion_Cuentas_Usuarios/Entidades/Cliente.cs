using System.ComponentModel.DataAnnotations;

namespace Gestion_Cuentas_Usuarios.Entidades
{
    public class Cliente
    {
        [Key]
        public int ID { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO { get; set; }
        public int ESTADO { get; set; }

    }
}

