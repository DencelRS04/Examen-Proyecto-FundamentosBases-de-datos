using System;
using System.Windows.Forms;
using ATM.Datos;
using ATM.Entidades;

namespace ATM.Presentacion
{
    public partial class FrmInformacionCliente : Form
    {
        private SesionATM sesion;
        private CuentaDatos datos = new CuentaDatos();

        public FrmInformacionCliente(SesionATM sesionActual)
        {
            InitializeComponent();
            sesion = sesionActual;

            this.Load += FrmInformacionCliente_Load;
        }

        private void FrmInformacionCliente_Load(object sender, EventArgs e)
        {
            if (sesion != null)
            {
                lblCuenta.Text = "Cuenta actual: " + sesion.NumeroCuenta;
            }

            dgvCliente.ReadOnly = true;
            dgvCliente.AllowUserToAddRows = false;
            dgvCliente.AllowUserToDeleteRows = false;
            dgvCliente.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCliente.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (sesion == null)
            {
                MessageBox.Show("No hay una sesión activa.");
                return;
            }

            dgvCliente.DataSource = datos.ConsultarInformacionCliente(sesion.NumeroCuenta);

            if (dgvCliente.Rows.Count == 0 || dgvCliente.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("No se encontró información del cliente.");
            }
        }

        private void dgvCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void lblCuenta_Click(object sender, EventArgs e)
        {
        }
    }
}