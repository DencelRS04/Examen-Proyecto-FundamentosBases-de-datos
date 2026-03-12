using System.Data.SqlClient;

namespace ATM.Datos
{
    public class Conexion
    {
        private string cadena = "Server=(localdb)\\MSSQLLocalDB.;Database=ATM;Trusted_Connection=True;";

        public SqlConnection ObtenerConexion()
        {
            SqlConnection cn = new SqlConnection(cadena);
            return cn;
        }
    }
}