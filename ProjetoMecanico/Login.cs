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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();

            usuario.Usr = txtUsuario.Text;
            usuario.Senha = Global.CriptografarSenha(txtSenha.Text);

            try
            {
                if (usuario.Autenticar())
                {
                    if (usuario.PerfilId == 3)
                    {
                        MessageBox.Show("Acesso restrito!\nSomente a Funcionários!", "Permissão Negada",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    MessageBox.Show("Login efetuado com sucesso",
                        "Login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    Global.UsuarioLogadoId = usuario.UsuarioId;
                    Global.UsuarioLogadoNome = usuario.Nome;
                    usuario.Consultar();

                   
                    if (usuario.PerfilId == 1)
                    {
                        if(MessageBox.Show("Deseja continuar como Administrador?", this.Text, MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        {
                            frmPedidos frm = new frmPedidos();
                            frm.ShowDialog();
                        }
                        else
                        {
                            frmCadastro frm = new frmCadastro();
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        frmPedidos frm = new frmPedidos();
                        frm.ShowDialog();
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Dados incorretos",
                        "Login",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    txtUsuario.Focus();
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro-->" + ex.Message,
                    "Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void btnEsqueceuSenha_Click(object sender, EventArgs e)
        {

            Usuario usr = new Usuario();
            usr.Usr = txtUsuario.Text;
            usr.Consultar();
            if (!usr.Autenticar())
            {
                MessageBox.Show("Usuario inválido! \nPreencha o campo Usuario", "Erro de Preenchimento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtUsuario.Text != string.Empty)
            {
                Global.NomeUsuario = txtUsuario.Text;
            }
            

            frmRedefinirSenha frm = new frmRedefinirSenha();
            frm.ShowDialog();

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
        }
    }
}
