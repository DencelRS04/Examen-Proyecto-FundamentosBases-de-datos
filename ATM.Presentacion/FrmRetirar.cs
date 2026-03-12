using System;
using System.Windows.Forms;
using ATM.Datos;
using System.Data;
using ATM.Entidades;

namespace ATM.Presentacion
{
    public partial class FrmRetirar : Form
    {
        CuentaDatos datos = new CuentaDatos();

        public FrmRetirar()
        {
            InitializeComponent();
        }

        // Constructor overload to accept a session and prefill the account number
        public FrmRetirar(SesionATM sesionActual)
        {
            InitializeComponent();
            if (sesionActual != null)
                txtNumeroCuenta.Text = sesionActual.NumeroCuenta;
        }

        private void btnRetirar_Click(object sender, EventArgs e)
        {
            string numeroCuenta = txtNumeroCuenta.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();
            decimal monto;

            if (numeroCuenta == "")
            {
                MessageBox.Show("Digite el número de cuenta.");
                txtNumeroCuenta.Focus();
                return;
            }

            long cuentaNumerica;
            if (!long.TryParse(numeroCuenta, out cuentaNumerica))
            {
                MessageBox.Show("El número de cuenta solo debe contener números.");
                txtNumeroCuenta.Focus();
                txtNumeroCuenta.SelectAll();
                return;
            }

            if (txtMonto.Text.Trim() == "")
            {
                MessageBox.Show("Digite el monto.");
                txtMonto.Focus();
                return;
            }

            if (!decimal.TryParse(txtMonto.Text.Trim(), out monto))
            {
                MessageBox.Show("El monto debe ser numérico.");
                txtMonto.Focus();
                txtMonto.SelectAll();
                return;
            }

            if (monto <= 0)
            {
                MessageBox.Show("El monto debe ser mayor que cero.");
                txtMonto.Focus();
                txtMonto.SelectAll();
                return;
            }

            if (descripcion == "")
            {
                descripcion = "Retiro en cajero";
            }

            // Obtain account and client ids from the account number
            DataTable dt = datos.ConsultarSaldo(numeroCuenta);

            if (dt == null || dt.Rows.Count == 0 || dt.Rows[0]["Id_cuenta"] == null || dt.Rows[0]["Id_cuenta"] == DBNull.Value)
            {
                MessageBox.Show("Cuenta no encontrada.");
                txtNumeroCuenta.Focus();
                txtNumeroCuenta.SelectAll();
                return;
            }

            int idCuenta = Convert.ToInt32(dt.Rows[0]["Id_cuenta"]);
            int idCliente = Convert.ToInt32(dt.Rows[0]["Id_cliente"]);

            string resultado = datos.Retirar(idCuenta, idCliente, monto, descripcion);

            if (resultado == "OK")
            {
                MessageBox.Show("Retiro realizado correctamente.");
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show(resultado);
                txtNumeroCuenta.Focus();
            }
        }

        private void LimpiarCampos()
        {
            txtNumeroCuenta.Clear();
            txtMonto.Clear();
            txtDescripcion.Clear();
            txtNumeroCuenta.Focus();
        }

        private void txtNumeroCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
                return;

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && txtMonto.Text.Contains("."))
            {
                e.Handled = true;
            }
        }
    }
}