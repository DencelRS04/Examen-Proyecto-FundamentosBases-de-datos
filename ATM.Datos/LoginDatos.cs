using System;
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

                string queryTarjeta = @"SELECT 
                                            T.Id_tarjeta,
                                            T.Id_cuenta,
                                            T.Pin,
                                            T.IntentosFallidos,
                                            T.Estado
                                        FROM Tarjetas T
                                        WHERE T.Numero_tarjeta = @NumeroTarjeta";

                SqlCommand cmd = new SqlCommand(queryTarjeta, cn);
                cmd.Parameters.AddWithValue("@NumeroTarjeta", numeroTarjeta);

                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.Read())
                {
                    mensaje = "Tarjeta no encontrada.";
                    return null;
                }

                int idTarjeta = Convert.ToInt32(dr["Id_tarjeta"]);
                int idCuenta = Convert.ToInt32(dr["Id_cuenta"]);
                string pinDB = dr["Pin"].ToString();
                int intentosFallidos = Convert.ToInt32(dr["IntentosFallidos"]);
                bool estadoTarjeta = Convert.ToBoolean(dr["Estado"]);

                dr.Close();

                if (!estadoTarjeta)
                {
                    mensaje = "La tarjeta está bloqueada.";
                    return null;
                }

                if (pinDB != pin)
                {
                    intentosFallidos++;

                    string updateIntentos = @"UPDATE Tarjetas
                                              SET IntentosFallidos = @Intentos
                                              WHERE Id_tarjeta = @IdTarjeta";

                    SqlCommand cmdIntentos = new SqlCommand(updateIntentos, cn);
                    cmdIntentos.Parameters.AddWithValue("@Intentos", intentosFallidos);
                    cmdIntentos.Parameters.AddWithValue("@IdTarjeta", idTarjeta);
                    cmdIntentos.ExecuteNonQuery();

                    if (intentosFallidos >= 3)
                    {
                        string bloquear = @"UPDATE Tarjetas
                                            SET Estado = 0
                                            WHERE Id_tarjeta = @IdTarjeta";

                        SqlCommand cmdBloquear = new SqlCommand(bloquear, cn);
                        cmdBloquear.Parameters.AddWithValue("@IdTarjeta", idTarjeta);
                        cmdBloquear.ExecuteNonQuery();

                        mensaje = "Tarjeta bloqueada por 3 intentos fallidos.";
                        return null;
                    }

                    mensaje = "PIN incorrecto.";
                    return null;
                }

                string resetIntentos = @"UPDATE Tarjetas
                                         SET IntentosFallidos = 0
                                         WHERE Id_tarjeta = @IdTarjeta";

                SqlCommand cmdReset = new SqlCommand(resetIntentos, cn);
                cmdReset.Parameters.AddWithValue("@IdTarjeta", idTarjeta);
                cmdReset.ExecuteNonQuery();

                string querySesion = @"SELECT 
                                            C.Id_cuenta,
                                            C.Id_cliente,
                                            C.Numero_cuenta
                                       FROM Cuentas C
                                       WHERE C.Id_cuenta = @IdCuenta";

                SqlCommand cmdSesion = new SqlCommand(querySesion, cn);
                cmdSesion.Parameters.AddWithValue("@IdCuenta", idCuenta);

                SqlDataReader drSesion = cmdSesion.ExecuteReader();

                if (drSesion.Read())
                {
                    sesion = new SesionATM
                    {
                        IdCuenta = Convert.ToInt32(drSesion["Id_cuenta"]),
                        IdCliente = Convert.ToInt32(drSesion["Id_cliente"]),
                        IdTarjeta = idTarjeta,
                        NumeroCuenta = drSesion["Numero_cuenta"].ToString(),
                        NumeroTarjeta = numeroTarjeta
                    };
                }

                drSesion.Close();
            }

            return sesion;
        }
    }
}