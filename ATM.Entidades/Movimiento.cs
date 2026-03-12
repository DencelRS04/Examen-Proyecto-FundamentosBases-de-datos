using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Entidades
{
    public class Movimiento
    {
        public int Id_movimiento { get; set; }
        public int Id_cliente { get; set; }
        public int Id_cuenta { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo_movimiento { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
    }
}
