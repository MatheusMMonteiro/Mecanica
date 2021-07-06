using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjetoMecanico
{
    public partial class frmCadastro : Form
    {
        Usuario usuario;
        public frmCadastro()
        {
            InitializeComponent();
        }
        Cliente cliente = new Cliente();

        private bool ValidarPreenchimento()
        {
            string mensagemErro = string.Empty;
            try
            {
                if (txtNome.Text == string.Empty)
                {
                    mensagemErro = "Preencha o campo nome.\n";
                }
                if (txtUsuario.Text == string.Empty)
                {
                    mensagemErro += "Preencha o campo usuário.\n";
                }               

                if (txtSenha.Text == string.Empty)
                {
                    mensagemErro += "Prencha o campo senha.\n";
                }
                else if (txtSenha.Text != txtConfirmar.Text)
                {
                    mensagemErro += "Confirmação da senha não confere.";
                }
                if (mensagemErro != string.Empty)
                {
                    MessageBox.Show(mensagemErro, "Cadastro de Usuários",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro --> " + ex.Message, "Cadastro de Usuários",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return mensagemErro == string.Empty;
        }
        
        
        private void btnGravar_Click(object sender, EventArgs e)
        {
            if (!ValidarPreenchimento())
            {
                return;
            }
            try
            {
                Usuario usuario = new Usuario();                       
                usuario.Nome = txtNome.Text;
                usuario.Usr = txtUsuario.Text;
                
                if (rdbAtivo.Checked)
                {
                    usuario.Ativo = true;
                }
                else
                {
                    usuario.Ativo = false;
                }
                if (usuario.Senha != txtSenha.Text)
                {
                    usuario.Senha = Global.CriptografarSenha(txtSenha.Text);
                }
                usuario.Email = txtEmail.Text;
                usuario.PerfilId = Convert.ToInt32(cboPerfil.SelectedValue);

                usuario.Gravar();
                MessageBox.Show("Usuário gravado com sucesso.", "Cadastro de usuários",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnLimpar.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro --> " + ex.Message, "Cadastro de Usuários",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CarregarPerfil()
        {
            try
            {
                cboPerfil.DataSource = Global.ConsultarPerfil();
                cboPerfil.DisplayMember = "Perfil";
                cboPerfil.ValueMember = "PerfilId";
                cboPerfil.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erro-->" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CarregarGrid()
        {
            try
            {
                grdPesquisa.DataSource = cliente.Consultar();

                foreach (DataGridViewColumn c in grdPesquisa.Columns)
                {
                    c.Visible = false;
                }
                grdPesquisa.Columns[1].Visible = true;
                grdPesquisa.Columns[2].Visible = true;
                grdPesquisa.Columns[3].Visible = true;
                grdPesquisa.Columns[16].Visible = true;

                grdPesquisa.Columns[1].Width = 100;
                grdPesquisa.Columns[2].Width = 255;
                grdPesquisa.Columns[3].Width = 255;
                grdPesquisa.Columns[16].Width = 258;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro--> " + ex.Message, this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLimpar_Click(object sender, EventArgs e)
        {
           txtPesquisa.Clear();
           txtNome.Clear();
           txtUsuario.Clear();
           txtSenha.Clear();
           txtConfirmar.Clear();
           txtEmail.Clear();
           cboPerfil.SelectedIndex = -1;
           rdbAtivo.Checked = true;
           
           txtPesquisa.Focus();
           CarregarGrid();           
        }

        private void grdPesquisa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                usuario = new Usuario();
                usuario.UsuarioId = Convert.ToInt32(grdPesquisa.SelectedRows[0].Cells[0].Value);
                usuario.Consultar();

                txtNome.Text = usuario.Nome;
                txtUsuario.Text = usuario.Usr;
                txtSenha.Text = usuario.Senha;
                txtConfirmar.Text = usuario.Senha;
                if (usuario.Ativo)
                {
                    rdbAtivo.Checked = true;
                }
                else
                {
                    rdbInativo.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro--> " + ex.Message, "Cadastro de Usuários",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCadastro_Load(object sender, EventArgs e)
        {
            CarregarPerfil();
            CarregarGrid();
        }

        private void rbdNome_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            usuario = new Usuario();
            if (rdbUsuario.Checked)
            {
                usuario.Nome = txtPesquisa.Text;
                CarregarGrid();
            }
            else if (rdbNome.Checked)
            {
                usuario.Nome = txtPesquisa.Text;
                CarregarGrid();
            }
        }
    }
}
