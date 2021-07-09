using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoMecanico
{
    public partial class frmRedefinirSenha : Form
    {
        public frmRedefinirSenha()
        {
            InitializeComponent();
        }        
        public int IdUsuario { get; set; }
        
        private bool ValidarPreenchimento()
        {
            string mensagemErro = string.Empty;
            try
            {
                if (!txtCodigo.Enabled)
                {
                    mensagemErro = "Confirme o Código de Preenchimento.\n";
                }
                else if (txtSenha.Text == string.Empty)
                {
                    mensagemErro += "Prencha o campo senha.\n";
                }
                else if (txtSenha.Text != txtConfirmar.Text)
                {
                    mensagemErro += "Confirmação da senha não confere.";
                }
                if (mensagemErro != string.Empty)
                {
                    MessageBox.Show(mensagemErro, "Redefinir Senha",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro --> " + ex.Message, "Redefinir Senha",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return mensagemErro == string.Empty;    
        }
        private void btnGravar_Click(object sender, EventArgs e)
        {
            ValidarPreenchimento();
            Usuario usr = new Usuario();
            usr.Usr = txtUsuario.Text;
            usr.Consultar();
            usr.Senha = Global.CriptografarSenha(txtSenha.Text);
            
            usr.Gravar();
            MessageBox.Show("Dados alterados com Sucesso!", "Redefinir Senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRedefinirSenha_Load(object sender, EventArgs e)
        {
            if (Global.NomeUsuario != string.Empty)
            {
                txtUsuario.Text = Global.NomeUsuario;
                btnReenviar.PerformClick();
            }
            
        }
        

        private void btnReenviar_Click(object sender, EventArgs e)
        {
            CodigoSenha cod = new CodigoSenha();
            Usuario usr = new Usuario();
            if (txtUsuario.Text == string.Empty)
            {
                MessageBox.Show("Preencha o Campo Usuario", "Erro de Preenchimento",  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            usr.Usr = txtUsuario.Text;
            usr.Consultar();
            if (!usr.Autenticar())
            {
                MessageBox.Show("Usuario inválido!", "Erro de Preenchimento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtUsuario.Enabled = false;

            cod.UsuarioId = usr.UsuarioId;
            cod.GerarCodigo();
            cod.GravarCodigo();
            
            //Enviar Email
            string email = usr.Email;
            string codigo = cod.Codigo;
            Global.EnviarEmailCodigo(email, codigo);

            MessageBox.Show("Código Enviado com Sucesso! \br Verifique seu E-mail!", "Redefinir Senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtCodigo.Enabled = true;            
        }

        private void btnConfirmarCodigo_Click(object sender, EventArgs e)
        {
            string mensagemErro = string.Empty;
            CodigoSenha cod = new CodigoSenha();
            Usuario usr = new Usuario();
            usr.Usr = txtUsuario.Text;
            usr.Consultar();
            cod.UsuarioId = usr.UsuarioId;
            cod.Consultar();
            string codigo = cod.Codigo;

            if (txtCodigo.Text == string.Empty)
            {
                mensagemErro = "Preencha o Campo Código";
            }
            if (txtCodigo.Text != codigo)
            {
                mensagemErro += "Código inválido!";
            }
            if (mensagemErro != string.Empty)
            {
                MessageBox.Show(mensagemErro, "Erro de Preenchimento", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Código Confirmado com Sucesso", "Redefinir Senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtSenha.Enabled = true;
            txtConfirmar.Enabled = true;
        }
    }
}
