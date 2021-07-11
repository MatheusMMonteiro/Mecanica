
namespace ProjetoMecanico
{
    partial class frmPedidos
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
            this.lblSituacao = new System.Windows.Forms.Label();
            this.cboSituacao = new System.Windows.Forms.ComboBox();
            this.grdPedidos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSituacao
            // 
            this.lblSituacao.AutoSize = true;
            this.lblSituacao.Location = new System.Drawing.Point(14, 16);
            this.lblSituacao.Name = "lblSituacao";
            this.lblSituacao.Size = new System.Drawing.Size(49, 13);
            this.lblSituacao.TabIndex = 0;
            this.lblSituacao.Text = "Situação";
            // 
            // cboSituacao
            // 
            this.cboSituacao.FormattingEnabled = true;
            this.cboSituacao.Location = new System.Drawing.Point(69, 13);
            this.cboSituacao.Name = "cboSituacao";
            this.cboSituacao.Size = new System.Drawing.Size(150, 21);
            this.cboSituacao.TabIndex = 1;
            this.cboSituacao.SelectedIndexChanged += new System.EventHandler(this.cboSituacao_SelectedIndexChanged);
            // 
            // grdPedidos
            // 
            this.grdPedidos.AllowUserToAddRows = false;
            this.grdPedidos.AllowUserToDeleteRows = false;
            this.grdPedidos.AllowUserToResizeColumns = false;
            this.grdPedidos.AllowUserToResizeRows = false;
            this.grdPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPedidos.Location = new System.Drawing.Point(12, 50);
            this.grdPedidos.MultiSelect = false;
            this.grdPedidos.Name = "grdPedidos";
            this.grdPedidos.ReadOnly = true;
            this.grdPedidos.RowHeadersVisible = false;
            this.grdPedidos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPedidos.Size = new System.Drawing.Size(285, 331);
            this.grdPedidos.TabIndex = 6;
            this.grdPedidos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPedidos_CellClick);
            // 
            // frmPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(312, 395);
            this.Controls.Add(this.grdPedidos);
            this.Controls.Add(this.cboSituacao);
            this.Controls.Add(this.lblSituacao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPedidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pedidos";
            this.Load += new System.EventHandler(this.frmPedidos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdPedidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSituacao;
        private System.Windows.Forms.ComboBox cboSituacao;
        private System.Windows.Forms.DataGridView grdPedidos;
    }
}