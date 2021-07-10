using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace ProjetoMecanico
{
        
    public static class Global

    {
        public static string NomeFuncionario = string.Empty;
        public static int NumeroPedido = 0;
        public static string NomeUsuario = string.Empty;
        public static int UsuarioLogadoId = 0;
        public static string UsuarioLogadoNome = string.Empty;

        public static string Servidor = string.Empty;
        public static string Banco = string.Empty;
        public static string StringConexao = string.Empty;

        public static void LerAppConfig()
        {
            try
            {
                Servidor = ConfigurationManager.AppSettings["Servidor"];
                Banco = ConfigurationManager.AppSettings["Banco"];

                StringConexao = string.Format(
                    "Data Source={0};Initial Catalog={1};" +
                    "Integrated Security=true;", Servidor, Banco);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string CriptografarSenha(string senha)
        {
            Byte[] byteTamanhoOriginal = ASCIIEncoding.Default.GetBytes(senha);

            MD5 md5 = new MD5CryptoServiceProvider();

            Byte[] byteTamanhoCriptografado = md5.ComputeHash(byteTamanhoOriginal);

            return Regex.Replace(BitConverter.ToString(
                byteTamanhoCriptografado), "-", "").ToLower();
        }
        

        public static DataTable ConsultarSituacao()
        {
            DataTable dataTable = new DataTable();
            AcessoBD acesso = new AcessoBD();
            try
            {
                string sql = "select SituacaoId, Situacao from tblSituacao \n";
                dataTable = acesso.Consultar(sql, new List<SqlParameter>());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return dataTable;
        }
        public static DataTable ConsultarPerfil()
        {
            DataTable dataTable = new DataTable();
            AcessoBD acesso = new AcessoBD();

            try
            {
                string sql = "select PerfilId, Perfil from tblPerfil \n";
                dataTable = acesso.Consultar(sql, new List<SqlParameter>());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return dataTable;
        }
        
        
        

        public static void EnviarEmailOcorrencia(string EnderecoEmail, string Resposta, string Pedido)
        {
            string Funcionario = UsuarioLogadoNome;
            MailMessage email = new MailMessage();
            email.From = new MailAddress("atendimento.mecanicatresirmaos@gmail.com");
            email.To.Add(EnderecoEmail);

            email.Subject = "CENTRAL DE ATENDIMENTO - Mecânica Três Irmãos";
            email.Body = Resposta+ "\n\n\nSeu Pedido gerou um Protocolo: #" + Pedido +
                "\n\n Atenciosamente, "+ Funcionario;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("atendimento.mecanicatresirmaos@gmail.com", "mecanica123");

            smtp.EnableSsl = true;
            smtp.Send(email);
        }
        public static void EnviarEmailCodigo(string EnderecoEmail, string Codigo)
        {
            MailMessage email = new MailMessage();
            email.From = new MailAddress("atendimento.mecanicatresirmaos@gmail.com");
            email.To.Add(EnderecoEmail);

            email.Subject = "Recuperação de Senha de Acesso";
            email.Body = "Seu token de acesso é:"+Codigo;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("atendimento.mecanicatresirmaos@gmail.com", "mecanica123");

            smtp.EnableSsl = true;
            smtp.Send(email);


        }





    }
}
