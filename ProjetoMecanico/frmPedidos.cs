using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoMecanico
{
    public partial class frmPedidos : Form
    {
        public frmPedidos()
        {
            InitializeComponent();
        }
        private void CarregarSituacao()
        {
            try
            {
                cboSituacao.DataSource = Global.ConsultarSituacao();
                cboSituacao.DisplayMember = "Situacao";
                cboSituacao.ValueMember = "SituacaoId";
                cboSituacao.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro-->" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        Pedidos pedidos = new Pedidos();
        Cliente cliente = new Cliente();

        private void PreencherPedidos()
        {
            try
            {
                pedidos.SituacaoId = Convert.ToInt32(cboSituacao.SelectedIndex);
                pedidos.Consultar();
                pedidos.PedidoId.ToString();
                cliente.ClienteId = pedidos.ClienteId;
                cliente.Consultar();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro--> " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CarregarGrid()
        {
            try
            {

                pedidos.SituacaoId = Convert.ToInt32(cboSituacao.SelectedIndex);
                grdPedidos.DataSource = pedidos.Consultar();
               

                foreach (DataGridViewColumn p in grdPedidos.Columns)
                {
                    p.Visible = false;
                }
                grdPedidos.Columns[0].Visible = true;
                grdPedidos.Columns[1].Visible = true;
                grdPedidos.Columns[2].Visible = true;
                grdPedidos.Columns[6].Visible = true;

                
                grdPedidos.Columns[0].Width = 60;
                grdPedidos.Columns[1].Width = 90;
                grdPedidos.Columns[2].Width = 90;
                grdPedidos.Columns[6].Width = 90;
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro--> " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grdPedidos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            pedidos.PedidoId = Convert.ToInt32(grdPedidos.SelectedRows[0].Cells[0].Value);
            
            Global.NumeroPedido = pedidos.PedidoId;
            frmAtendimentoPedidos frm = new frmAtendimentoPedidos();
            //frm.txtNumeroPedido.Text = grdPedidos.SelectedRows[0].Cells[0].Value.ToString();
            frm.ShowDialog();
            

        }


        private void frmPedidos_Load(object sender, EventArgs e)
        {
            CarregarSituacao();            
            Pedidos pedidos = new Pedidos();
        }

        private void txtPesquisar_TextChanged(object sender, EventArgs e)
        {
            PreencherPedidos();
             if (rdbPedido.Checked && txtPesquisar.Text.Length>0)
              {                
                  pedidos.PedidoId =Convert.ToInt32(txtPesquisar.Text);
                  CarregarGrid();                           
              }


        }

        private void cboSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarGrid();
        }

        private void rdbPedido_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtPesquisar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void rdbNome_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void grdPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
