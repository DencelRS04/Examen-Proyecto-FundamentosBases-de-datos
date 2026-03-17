using System;
using System.Windows.Forms;
using ATM.Datos;
using ATM.Entidades;

namespace ATM.Presentacion
{
    public partial class FrmMovimientos : Form
    {
        private SesionATM sesion;
        private CuentaDatos datos = new CuentaDatos();

        public FrmMovimientos(SesionATM sesionActual)
        {
            InitializeComponent();
            sesion = sesionActual;

            this.Load += FrmMovimientos_Load;
        }

        private void FrmMovimientos_Load(object sender, EventArgs e)
        {
            if (sesion != null)
            {
                lblCuenta.Text = "Cuenta actual: " + sesion.NumeroCuenta;
                dgvMovimientos.DataSource = datos.ConsultarMovimientos(sesion.NumeroCuenta);
            }

            dgvMovimientos.ReadOnly = true;
            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.AllowUserToDeleteRows = false;
            dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (sesion == null)
            {
                MessageBox.Show("No hay una sesión activa.");
                return;
            }

            dgvMovimientos.DataSource = datos.ConsultarMovimientos(sesion.NumeroCuenta);
        }

        private void FrmMovimientos_Load_1(object sender, EventArgs e)
        {
        }
    }
}