using System;
using System.Windows.Forms;
using ATM.Datos;
using ATM.Entidades;

namespace ATM.Presentacion
{
    public partial class FrmLoginATM : Form
    {
        private LoginDatos loginDatos = new LoginDatos();

        public FrmLoginATM()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string numeroTarjeta = txtNumeroTarjeta.Text.Trim();
            string pin = txtPin.Text.Trim();

            if (numeroTarjeta == "")
            {
                MessageBox.Show("Digite el número de tarjeta.");
                txtNumeroTarjeta.Focus();
                return;
            }

            if (numeroTarjeta.Length != 16)
            {
                MessageBox.Show("El número de tarjeta debe tener 16 dígitos.");
                txtNumeroTarjeta.Focus();
                txtNumeroTarjeta.SelectAll();
                return;
            }

            if (pin == "")
            {
                MessageBox.Show("Digite el PIN.");
                txtPin.Focus();
                return;
            }

            if (pin.Length != 4)
            {
                MessageBox.Show("El PIN debe tener 4 dígitos.");
                txtPin.Focus();
                txtPin.SelectAll();
                return;
            }

            string mensaje;
            SesionATM sesion = loginDatos.ValidarTarjetaPin(numeroTarjeta, pin, out mensaje);

            if (sesion != null)
            {
                FrmMenu menu = new FrmMenu(sesion);
                this.Hide();
                menu.ShowDialog();
                this.Show();

                txtPin.Clear();
                txtNumeroTarjeta.Clear();
                txtNumeroTarjeta.Focus();
            }
            else
            {
                MessageBox.Show(mensaje);
                txtPin.Clear();
                txtPin.Focus();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtNumeroTarjeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FrmLoginATM_Load(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtNumeroTarjeta_TextChanged(object sender, EventArgs e)
        {
        }
    }
}