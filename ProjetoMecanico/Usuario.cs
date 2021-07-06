using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace ProjetoMecanico
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Usr { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }        
        public bool Ativo { get; set; }
        public int PerfilId { get; set; }

        public Usuario()
        {
            UsuarioId = 0;
            Nome = string.Empty;
            Usr = string.Empty;
            Senha = string.Empty;
            Email = string.Empty;
            Ativo = false;
            PerfilId = 0;
        }

        AcessoBD acesso = new AcessoBD();
        DataTable dataTable = new DataTable();
        List<SqlParameter> variaveis = new List<SqlParameter>();
        string sql = string.Empty;

        public bool Autenticar()
        {
            bool resultado = false;
            try
            {
                sql = "select UsuarioId from tblUsuario \n";
                sql += "Where Usuario = @usuario \n";
                sql += "and Senha = @senha \n";
                sql += "and Ativo=1";

                variaveis.Clear();
                variaveis.Add(new SqlParameter("@usuario", Usr));
                variaveis.Add(new SqlParameter("@senha", Senha));

                dataTable = acesso.Consultar(sql, variaveis);

                if (dataTable.Rows.Count == 1)
                {
                    UsuarioId = Convert.ToInt32(dataTable.Rows[0]["UsuarioId"]);
                    Consultar();

                }

                resultado = dataTable.Rows.Count == 1;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return resultado;
        }

        public DataTable Consultar()
        {
            try
            {
                variaveis.Clear();

                sql = "select UsuarioId, Nome, Usuario, Senha, Email, Ativo, PerfilId \n";
                sql += "from tblUsuario \n";

                if (UsuarioId != 0)
                {
                    sql += "where UsuarioId = @usuarioId";
                    variaveis.Add(new SqlParameter("@usuarioId", UsuarioId));
                }
                else if (Usr != string.Empty)
                {
                    sql += "where Usuario = @usuario";
                    variaveis.Add(new SqlParameter("@usuario", Usr));
                }
                else if (Nome != string.Empty)
                {
                    sql += "where Nome like @nome";
                    variaveis.Add(new SqlParameter("@nome", '%' + Nome + '%'));
                }

                dataTable = acesso.Consultar(sql, variaveis);

                if (UsuarioId != 0 || (Usr != string.Empty && dataTable.Rows.Count == 1))
                {
                    UsuarioId = Convert.ToInt32(dataTable.Rows[0]["UsuarioId"]);
                    Nome = dataTable.Rows[0]["Nome"].ToString();
                    Usr = dataTable.Rows[0]["Usuario"].ToString();
                    Senha = dataTable.Rows[0]["Senha"].ToString();
                    Email = dataTable.Rows[0]["Email"].ToString();                    
                    Ativo = Convert.ToBoolean(dataTable.Rows[0]["ativo"]);
                    PerfilId = Convert.ToInt32(dataTable.Rows[0]["PerfilId"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dataTable;
        }
        public void Gravar()
        {
            try
            {
                variaveis.Clear();

                if (UsuarioId == 0)
                {
                    sql = "insert into tblUsuario (Nome, Usuario, Senha, Email, Ativo, PerfilId)\n";
                    sql += "values (@nome, @usuario, @senha, @Email, @ativo, @PerfilId)";
                }
                else
                {
                    sql = "update tblUsuario set \n";
                    sql += "Nome = @nome,\n";
                    sql += "Usuario = @usuario,\n";
                    sql += "Senha = @senha, \n";
                    sql += "Email = @Email, \n";
                    sql += "Ativo = @ativo \n";
                    sql += "PerfilId = @PerfilId, \n";
                    sql += "where UsuarioId = @usuarioId";
                    variaveis.Add(new SqlParameter("@usuarioId", UsuarioId));
                }

                variaveis.Add(new SqlParameter("@nome", Nome));
                variaveis.Add(new SqlParameter("@usuario", Usr));
                variaveis.Add(new SqlParameter("@senha", Senha));
                variaveis.Add(new SqlParameter("@email", Email));
                variaveis.Add(new SqlParameter("@ativo", Ativo));
                variaveis.Add(new SqlParameter("@PerfilId", PerfilId));


                acesso.Executar(sql, variaveis);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


    }
}
