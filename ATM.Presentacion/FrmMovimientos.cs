using System;
using System.Windows.Forms;
using ATM.Datos;
using ATM.Entidades;

namespace ATM.Presentacion
{
    public partial class FrmMovimientos : Form
    {
        private SesionATM sesion;
        CuentaDatos datos = new CuentaDatos();

        public FrmMovimientos(SesionATM sesionActual)
        {
            InitializeComponent();
            sesion = sesionActual;
        }

        private void FrmMovimientos_Load(object sender, EventArgs e)
        {
            lblCuenta.Text = "Cuenta actual: " + sesion.NumeroCuenta;
            dgvMovimientos.ReadOnly = true;
            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.AllowUserToDeleteRows = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvMovimientos.DataSource = datos.ConsultarMovimientos(sesion.NumeroCuenta);
        }
        private void FrmMovimientos_Load_1(object sender, EventArgs e)
        {

        }
    }
}