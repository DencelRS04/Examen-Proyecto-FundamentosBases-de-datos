namespace ATM.Presentacion
{
    partial class FrmConsultarSaldo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCuenta = new System.Windows.Forms.Label();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.dgvSaldo = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaldo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCuenta
            // 
            this.lblCuenta.AutoSize = true;
            this.lblCuenta.Font = new System.Drawing.Font("Tw Cen MT", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCuenta.Location = new System.Drawing.Point(29, 50);
            this.lblCuenta.Name = "lblCuenta";
            this.lblCuenta.Size = new System.Drawing.Size(159, 22);
            this.lblCuenta.TabIndex = 0;
            this.lblCuenta.Text = "Numero de cuenta:";
            this.lblCuenta.Click += new System.EventHandler(this.lblCuenta_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Font = new System.Drawing.Font("Tw Cen MT", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.Location = new System.Drawing.Point(662, 40);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(113, 32);
            this.btnConsultar.TabIndex = 2;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // dgvSaldo
            // 
            this.dgvSaldo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSaldo.Location = new System.Drawing.Point(12, 97);
            this.dgvSaldo.Name = "dgvSaldo";
            this.dgvSaldo.Size = new System.Drawing.Size(772, 316);
            this.dgvSaldo.TabIndex = 3;
            // 
            // FrmConsultarSaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvSaldo);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.lblCuenta);
            this.Name = "FrmConsultarSaldo";
            this.Text = "FrmConsultarSaldo";
            this.Load += new System.EventHandler(this.FrmConsultarSaldo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSaldo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCuenta;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DataGridView dgvSaldo;
    }
}