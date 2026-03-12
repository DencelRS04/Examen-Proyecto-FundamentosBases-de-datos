namespace ATM.Entidades
{
    public class Cuenta
    {
        public int Id_cuenta { get; set; }
        public int Id_cliente { get; set; }
        public string Numero_cuenta { get; set; }
        public decimal Saldo { get; set; }
        public string Tipo_cuenta { get; set; }
        public bool Estado { get; set; }
    }
}
