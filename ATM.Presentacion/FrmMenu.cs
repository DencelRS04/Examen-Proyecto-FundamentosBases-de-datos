using System;
using System.Windows.Forms;
using ATM.Entidades;

namespace ATM.Presentacion
{
    public partial class FrmMenu : Form
    {
        private SesionATM sesion;

        public FrmMenu(SesionATM sesionActual)
        {
            InitializeComponent();
            sesion = sesionActual;
        }

        private void btnConsultarSaldo_Click(object sender, EventArgs e)
        {
            FrmConsultarSaldo frm = new FrmConsultarSaldo(sesion);
            frm.ShowDialog();
        }

        private void btnDepositar_Click(object sender, EventArgs e)
        {
            FrmDepositar frm = new FrmDepositar(sesion);
            frm.ShowDialog();
        }

        private void btnRetirar_Click(object sender, EventArgs e)
        {
            FrmRetirar frm = new FrmRetirar(sesion);
            frm.ShowDialog();
        }

        private void btnMovimientos_Click(object sender, EventArgs e)
        {
            FrmMovimientos frm = new FrmMovimientos(sesion);
            frm.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            // Optional initialization when menu loads.
        }
    }
}