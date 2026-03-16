using System;
using System.Windows.Forms;
using ATM.Datos;
using ATM.Entidades;

namespace ATM.Presentacion
{
    public partial class FrmConsultarSaldo : Form
    {
        private SesionATM sesion;
        private CuentaDatos datos = new CuentaDatos();

        public FrmConsultarSaldo(SesionATM sesionActual)
        {
            InitializeComponent();
            sesion = sesionActual;
        }

        private void FrmConsultarSaldo_Load(object sender, EventArgs e)
        {
            if (sesion != null)
            {
                lblCuenta.Text = "Cuenta actual: " + sesion.NumeroCuenta;
            }

            dgvSaldo.ReadOnly = true;
            dgvSaldo.AllowUserToAddRows = false;
            dgvSaldo.AllowUserToDeleteRows = false;
            dgvSaldo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSaldo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (sesion == null)
            {
                MessageBox.Show("No hay una sesión activa.");
                return;
            }

            dgvSaldo.DataSource = datos.ConsultarSaldo(sesion.NumeroCuenta);

            if (dgvSaldo.Rows.Count == 0 || dgvSaldo.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("Cuenta no encontrada.");
            }
        }

        private void lblCuenta_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtNumeroCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dgvSaldo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}