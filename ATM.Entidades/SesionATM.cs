using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Entidades
{
    public class SesionATM
    {
        public int IdCuenta { get; set; }
        public int IdCliente { get; set; }
        public string NumeroCuenta { get; set; }
        public string NumeroTarjeta { get; set; }
    }
}