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
        public string usuario { get; set; }
        
        private bool ValidarPreenchimento()
        {
            string mensagemErro = string.Empty;
            try
            {
                if (txtUsuario.Text == string.Empty)
                {
                    mensagemErro = "Preencha o campo Usuário.\n";
                }
                if (txtCodigo.Text == string.Empty)
                {
                    mensagemErro += "Preencha o campo Codigo.\n";
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

            usr.Usr = txtUsuario.Text;
            usr.Consultar();
            
            cod.UsuarioId = usr.UsuarioId;
            cod.GerarCodigo();
            cod.GravarCodigo();

            
        }

        private void btnConfirmarCodigo_Click(object sender, EventArgs e)
        {

        }
    }
}
