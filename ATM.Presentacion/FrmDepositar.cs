using System;
using System.Windows.Forms;
using System.Data;
using ATM.Datos;
using ATM.Entidades;

namespace ATM.Presentacion
{
    public partial class FrmDepositar : Form
    {
        CuentaDatos datos = new CuentaDatos();

        public FrmDepositar()
        {
            InitializeComponent();
        }

        // Constructor overload to accept a session and prefill the account number
        public FrmDepositar(SesionATM sesionActual)
        {
            InitializeComponent();
            if (sesionActual != null)
                txtNumeroCuenta.Text = sesionActual.NumeroCuenta;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
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
                descripcion = "Depósito en cajero";
            }

            // Obtener Id_cuenta e Id_cliente antes de llamar a Depositar
            DataTable dt = datos.ObtenerDatosCuentaPorNumero(numeroCuenta); if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("No se encontró la cuenta. Verifique el número de cuenta.");
                txtNumeroCuenta.Focus();
                return;
            }

            int idCuenta = Convert.ToInt32(dt.Rows[0]["Id_cuenta"]);
            int idCliente = Convert.ToInt32(dt.Rows[0]["Id_cliente"]);

            bool resultado = datos.Depositar(idCuenta, idCliente, monto, descripcion);

            if (resultado)
            {
                MessageBox.Show("Depósito realizado correctamente.");
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo realizar el depósito. Verifique la cuenta.");
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

        private void FrmDepositar_Load(object sender, EventArgs e)
        {

        }
    }
}