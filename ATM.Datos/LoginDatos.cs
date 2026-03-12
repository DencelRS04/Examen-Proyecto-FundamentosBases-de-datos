using System.Data.SqlClient;
using ATM.Entidades;

namespace ATM.Datos
{
    public class LoginDatos
    {
        Conexion conexion = new Conexion();

        public SesionATM ValidarTarjetaPin(string numeroTarjeta, string pin, out string mensaje)
        {
            SesionATM sesion = null;
            mensaje = "";

            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                cn.Open();

                string query = @"SELECT Id_cuenta, Id_cliente, Numero_cuenta, Pin, IntentosFallidos, Estado
                         FROM Cuentas
                         WHERE Numero_tarjeta = @NumeroTarjeta";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@NumeroTarjeta", numeroTarjeta);

                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.Read())
                {
                    mensaje = "Tarjeta no encontrada.";
                    return null;
                }

                int idCuenta = (int)dr["Id_cuenta"];
                int idCliente = (int)dr["Id_cliente"];
                string numeroCuenta = dr["Numero_cuenta"].ToString();
                string pinDB = dr["Pin"].ToString();
                int idxIntentos = dr.GetOrdinal("IntentosFallidos");
                int intentos = dr.IsDBNull(idxIntentos) ? 0 : dr.GetInt32(idxIntentos);
                bool estado = (bool)dr["Estado"];

                dr.Close();

                if (!estado)
                {
                    mensaje = "Tarjeta bloqueada.";
                    return null;
                }

                if (pin != pinDB)
                {
                    intentos++;

                    string updateIntento = @"UPDATE Cuentas
                                     SET IntentosFallidos = @Intentos
                                     WHERE Id_cuenta = @IdCuenta";

                    SqlCommand cmdIntento = new SqlCommand(updateIntento, cn);
                    cmdIntento.Parameters.AddWithValue("@Intentos", intentos);
                    cmdIntento.Parameters.AddWithValue("@IdCuenta", idCuenta);
                    cmdIntento.ExecuteNonQuery();

                    if (intentos >= 3)
                    {
                        string bloquear = @"UPDATE Cuentas
                                    SET Estado = 0
                                    WHERE Id_cuenta = @IdCuenta";

                        SqlCommand cmdBloquear = new SqlCommand(bloquear, cn);
                        cmdBloquear.Parameters.AddWithValue("@IdCuenta", idCuenta);
                        cmdBloquear.ExecuteNonQuery();

                        mensaje = "Tarjeta bloqueada por 3 intentos fallidos.";
                    }
                    else
                    {
                        mensaje = "PIN incorrecto. Intentos: " + intentos;
                    }

                    return null;
                }

                string resetIntentos = @"UPDATE Cuentas
                                 SET IntentosFallidos = 0
                                 WHERE Id_cuenta = @IdCuenta";

                SqlCommand cmdReset = new SqlCommand(resetIntentos, cn);
                cmdReset.Parameters.AddWithValue("@IdCuenta", idCuenta);
                cmdReset.ExecuteNonQuery();

                sesion = new SesionATM
                {
                    IdCuenta = idCuenta,
                    IdCliente = idCliente,
                    NumeroCuenta = numeroCuenta,
                    NumeroTarjeta = numeroTarjeta
                };
            }

            return sesion;
        }
    }
}