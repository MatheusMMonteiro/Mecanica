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
    public partial class frmAtendimentoPedidos : Form
    {
        public frmAtendimentoPedidos()
        {
            InitializeComponent();
        }
        
        Cliente cliente = new Cliente();
        Pedidos pedidos = new Pedidos();
        Veiculo veiculo = new Veiculo();
        
        private void PreencherPedido()
        {
            try
            {
                pedidos.PedidoId = 2;
                pedidos.Consultar();
                pedidos.PedidoId.ToString();
                cliente.ClienteId = pedidos.ClienteId;
                cliente.Consultar();

                veiculo.ClienteId = pedidos.ClienteId;
                veiculo.ConsultarVeiculo();

                veiculo.ConsultarModelo();
                txtModelo.Text = veiculo.Modelo;

                veiculo.ConsultarMarca();
                txtMarca.Text = veiculo.Marca; 

                
                
                txtNomeCliente.Text = cliente.Nome;
                txtData.Text = cliente.DataNascimento.ToString();
                

                //txtModelo.Text = Global.ConsultarModelo().ToString();
                txtOcorrencia.Text = pedidos.Ocorrencia;
                txtNumeroPedido.Text = pedidos.PedidoId.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro--> " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            
        }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResponderOcorrencia_Click(object sender, EventArgs e)
        {
            frmAtendimentoPedidos frm = new frmAtendimentoPedidos();
            frm.Size = new Size(549, 605);
           

        }

        private void frmAtendimentoPedidos_Load(object sender, EventArgs e)
        {
            PreencherPedido();
        }

        private void btnEncerrarOcorrencia_Click(object sender, EventArgs e)
        {
            if(txtResposta.Text!= string.Empty)
            {
                MessageBox.Show("Erro de Preenchimento", "Campo Resposta vazio!", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            pedidos.Ocorrencia = txtOcorrencia.Text;
            pedidos.SituacaoId = 2;
            pedidos.Gravar();
            Usuario u = new Usuario();
            Global.EnviarEmailOcorrencia(u.Email, pedidos.Ocorrencia, pedidos.PedidoId.ToString());


        }
    }
}
