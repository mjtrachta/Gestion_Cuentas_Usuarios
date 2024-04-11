namespace Gestion_Cuentas_Usuarios.DTO
{
    public class TransaccionesDto
    {
        public long ID_TRANSACCION { get; set; }
        public DateTime FECHA_HORA { get; set; }
        public decimal MONTO { get; set; }
        public int ID_CUENTA { get; set; }
        public int ID_TIPO_MOVIMIENTO { get; set; }

        //Join de tipo transaccion
        public string NOMBRE_TIPO_MOVIMIENTO { get; set; }
    }
}
