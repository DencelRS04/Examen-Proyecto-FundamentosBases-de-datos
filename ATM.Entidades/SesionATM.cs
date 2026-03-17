namespace ATM.Entidades
{
    public class SesionATM
    {
        public int IdCuenta { get; set; }
        public int IdCliente { get; set; }
        public int IdTarjeta { get; set; }
        public string NumeroCuenta { get; set; }
        public string NumeroTarjeta { get; set; }
    }
}