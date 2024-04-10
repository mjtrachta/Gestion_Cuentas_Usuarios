namespace Gestion_Cuentas_Usuarios.Service.Interface
{
    public interface ITransaccionesService
    {
        Task<bool> RealizarDeposito(int idCuenta, decimal monto);
    }
}
