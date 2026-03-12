using ATM.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ATM.Datos
{
    public class CuentaDatos
    {
        Conexion conexion = new Conexion();

        public DataTable ConsultarSaldo(string numeroCuenta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                string query = @"SELECT 
                                    C.Id_cuenta,
                                    C.Id_cliente,
                                    C.Numero_cuenta,
                                    CL.Nombre,
                                    CL.Apellido1,
                                    CL.Apellido2,
                                    TC.Descripcion AS Tipo_cuenta,
                                    C.Saldo,
                                    CASE 
                                        WHEN C.Estado = 1 THEN 'Activa'
                                        ELSE 'Inactiva'
                                    END AS Estado
                                 FROM Cuentas C
                                 INNER JOIN Clientes CL ON C.Id_cliente = CL.Id_cliente
                                 INNER JOIN TipoCuenta TC ON C.Id_tipo_cuenta = TC.Id_tipo_cuenta
                                 WHERE C.Numero_cuenta = @NumeroCuenta";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@NumeroCuenta", numeroCuenta);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public bool Depositar(int idCuenta, int idCliente, decimal monto, string descripcion)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction();

                try
                {
                    if (monto <= 0)
                    {
                        tr.Rollback();
                        return false;
                    }

                    string update = @"UPDATE Cuentas
                                      SET Saldo = Saldo + @Monto
                                      WHERE Id_cuenta = @IdCuenta AND Estado = 1";

                    SqlCommand cmdUpdate = new SqlCommand(update, cn, tr);
                    cmdUpdate.Parameters.AddWithValue("@Monto", monto);
                    cmdUpdate.Parameters.AddWithValue("@IdCuenta", idCuenta);

                    int filas = cmdUpdate.ExecuteNonQuery();

                    if (filas == 0)
                    {
                        tr.Rollback();
                        return false;
                    }

                    string insert = @"INSERT INTO Movimientos
                                      (Id_cliente, Id_cuenta, Fecha, Id_tipo_movimiento, Monto, Descripcion)
                                      VALUES
                                      (@IdCliente, @IdCuenta, GETDATE(), 1, @Monto, @Descripcion)";

                    SqlCommand cmdInsert = new SqlCommand(insert, cn, tr);
                    cmdInsert.Parameters.AddWithValue("@IdCliente", idCliente);
                    cmdInsert.Parameters.AddWithValue("@IdCuenta", idCuenta);
                    cmdInsert.Parameters.AddWithValue("@Monto", monto);
                    cmdInsert.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmdInsert.ExecuteNonQuery();

                    tr.Commit();
                    return true;
                }
                catch
                {
                    tr.Rollback();
                    return false;
                }
            }
        }

        public string Retirar(int idCuenta, int idCliente, decimal monto, string descripcion)
        {
            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                cn.Open();
                SqlTransaction tr = cn.BeginTransaction();

                try
                {
                    if (monto <= 0)
                    {
                        tr.Rollback();
                        return "El monto debe ser mayor que cero.";
                    }

                    decimal saldo = 0;

                    string buscar = @"SELECT Saldo
                                      FROM Cuentas
                                      WHERE Id_cuenta = @IdCuenta AND Estado = 1";

                    SqlCommand cmdBuscar = new SqlCommand(buscar, cn, tr);
                    cmdBuscar.Parameters.AddWithValue("@IdCuenta", idCuenta);

                    object resultado = cmdBuscar.ExecuteScalar();

                    if (resultado == null)
                    {
                        tr.Rollback();
                        return "La cuenta no existe o está inactiva.";
                    }

                    saldo = Convert.ToDecimal(resultado);

                    if (saldo < monto)
                    {
                        tr.Rollback();
                        return "Saldo insuficiente.";
                    }

                    string update = @"UPDATE Cuentas
                                      SET Saldo = Saldo - @Monto
                                      WHERE Id_cuenta = @IdCuenta";

                    SqlCommand cmdUpdate = new SqlCommand(update, cn, tr);
                    cmdUpdate.Parameters.AddWithValue("@Monto", monto);
                    cmdUpdate.Parameters.AddWithValue("@IdCuenta", idCuenta);
                    cmdUpdate.ExecuteNonQuery();

                    string insert = @"INSERT INTO Movimientos
                                      (Id_cliente, Id_cuenta, Fecha, Id_tipo_movimiento, Monto, Descripcion)
                                      VALUES
                                      (@IdCliente, @IdCuenta, GETDATE(), 2, @Monto, @Descripcion)";

                    SqlCommand cmdInsert = new SqlCommand(insert, cn, tr);
                    cmdInsert.Parameters.AddWithValue("@IdCliente", idCliente);
                    cmdInsert.Parameters.AddWithValue("@IdCuenta", idCuenta);
                    cmdInsert.Parameters.AddWithValue("@Monto", monto);
                    cmdInsert.Parameters.AddWithValue("@Descripcion", descripcion);
                    cmdInsert.ExecuteNonQuery();

                    tr.Commit();
                    return "OK";
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    return ex.Message;
                }
            }
        }

        public DataTable ConsultarMovimientos(string numeroCuenta)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = conexion.ObtenerConexion())
            {
                string query = @"SELECT 
                                    M.Id_movimiento,
                                    M.Fecha,
                                    TM.Descripcion AS Tipo_movimiento,
                                    M.Monto,
                                    M.Descripcion
                                 FROM Movimientos M
                                 INNER JOIN Cuentas C ON M.Id_cuenta = C.Id_cuenta
                                 INNER JOIN TipoMovimiento TM ON M.Id_tipo_movimiento = TM.Id_tipo_movimiento
                                 WHERE C.Numero_cuenta = @NumeroCuenta
                                 ORDER BY M.Fecha DESC, M.Id_movimiento DESC";

                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@NumeroCuenta", numeroCuenta);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }
    }
}