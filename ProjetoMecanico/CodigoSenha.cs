using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace ProjetoMecanico
{
    public class CodigoSenha
    {
        public int UsuarioId { get; set; }
        public string Codigo { get; set; }

        public CodigoSenha()
        {
            UsuarioId = 0;
            Codigo = string.Empty;
        }

        AcessoBD acessoBD = new AcessoBD();
        DataTable dataTable = new DataTable();
        List<SqlParameter> variaveis = new List<SqlParameter>();
        string sql = string.Empty;

        public DataTable GravarCodigo()
        {
            try
            {
                variaveis.Clear();
                using (TransactionScope Transacao = new TransactionScope())
                {
                    if (UsuarioId != 0)
                        sql = "delete from tblRecuperarSenha where UsuarioId=@UsuarioId \n";
                    sql += "insert into tblRecuperarSenha(Codigo,UsuarioId) values (@Codigo, @UsuarioId) select @@identity";
                    variaveis.Add(new SqlParameter("@Codigo", Codigo));
                    variaveis.Add(new SqlParameter("@UsuarioId", UsuarioId));

                    if (UsuarioId != 0)
                        /*{
                            UsuarioId = Convert.ToInt32(acessoBD.Executar(variaveis, sql));
                        }
                        else
                        {*/
                        acessoBD.Executar(sql, variaveis);
                    // }
                    Transacao.Complete();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dataTable;
        }
        public DataTable Consultar()
        {
            try
            {
                variaveis.Clear();
                sql = "select Codigo, UsuarioId from tblRecuperarSenha where UsuarioId=@UsuarioId";
                variaveis.Add(new SqlParameter("@usuarioId", UsuarioId));

                dataTable = acessoBD.Consultar(sql, variaveis);

                if (UsuarioId != 0)
                {
                    UsuarioId = Convert.ToInt32(dataTable.Rows[0]["UsuarioId"]);
                    Codigo = dataTable.Rows[0]["Codigo"].ToString();
                }

            }
            catch (Exception ex)
            {


                throw new Exception(ex.Message);
            }
            return dataTable;
        }
        public void GerarCodigo()
        {
            Random rand = new Random();
            Codigo = rand.Next(111111, 999999).ToString();
        }
    }
}
