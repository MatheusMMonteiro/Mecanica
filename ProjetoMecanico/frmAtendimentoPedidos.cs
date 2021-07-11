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
        Usuario usuario = new Usuario();
        
        private void PreencherPedido()
        {
            try
            {
                pedidos.PedidoId = Global.NumeroPedido;
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

                txtResposta.Text = pedidos.Resposta;
                
                txtNomeCliente.Text = cliente.Nome;
                txtData.Text = cliente.DataNascimento.ToString();
                

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
            

            btnEncerrarOcorrencia.Enabled = true;
            btnCancelarOcorrencia.Enabled = true;
            btnResponderOcorrencia.Enabled = false;
            btnFechar.Enabled = false;
            txtResposta.Enabled = true;
           

        }

        private void frmAtendimentoPedidos_Load(object sender, EventArgs e)
        {
            PreencherPedido();
            if (txtResposta.Text != string.Empty)
            {
                btnResponderOcorrencia.Enabled = false;
                btnCancelarOcorrencia.Enabled = false;
                btnEncerrarOcorrencia.Enabled = false;
                btnFechar.Enabled = true;
                txtResposta.Enabled = false;
            }
        }

        private void btnEncerrarOcorrencia_Click(object sender, EventArgs e)
        {
            if(txtResposta.Text== string.Empty)
            {
                MessageBox.Show("Erro de Preenchimento", "Campo Resposta vazio!", MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            try
            {
                pedidos.PedidoId = Convert.ToInt32(txtNumeroPedido.Text);
                pedidos.Consultar();

                cliente.ClienteId = pedidos.ClienteId;
                cliente.Consultar();

                usuario.UsuarioId = cliente.UsuarioId;
                usuario.Consultar();

                pedidos.Resposta = txtResposta.Text;
                pedidos.SituacaoId = 2;
                pedidos.Gravar();

                
                Global.EnviarEmailOcorrencia(usuario.Email, pedidos.Resposta, pedidos.PedidoId.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro--> " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnResponderOcorrencia.Enabled = false;
            btnCancelarOcorrencia.Enabled = false;
            btnEncerrarOcorrencia.Enabled = false;
            btnFechar.Enabled = true;
            txtResposta.Enabled = false;

            MessageBox.Show("Ocorrência Encerrada com sucesso!", "Atendimento de Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;



        }

        private void btnCancelarOcorrencia_Click(object sender, EventArgs e)
        {
            txtResposta.Clear();
            txtResposta.Enabled = false;
            btnCancelarOcorrencia.Enabled = false;
            btnEncerrarOcorrencia.Enabled = false;
            btnResponderOcorrencia.Enabled = true;
            btnFechar.Enabled = true;
        }
    }
}
