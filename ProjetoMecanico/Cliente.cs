using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace ProjetoMecanico
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; } 
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Telefone { get; set; }
        public int CidadeId { get; set; }
        public int UsuarioId { get; set; }
        

        public Cliente()
        {
            ClienteId = 0;
            Nome = string.Empty;
            CPF = string.Empty;
            DataNascimento = Convert.ToDateTime("01/01/1900");
            Endereco = string.Empty;
            Numero = string.Empty;
            Complemento = string.Empty;
            Bairro = string.Empty;
            CEP = string.Empty;
            Telefone = string.Empty;
            CidadeId = 0;
            UsuarioId = 0;

        }
        AcessoBD acessoBD = new AcessoBD();
        DataTable dataTable = new DataTable();
        List<SqlParameter> variaveis = new List<SqlParameter>();
        string sql = string.Empty;

        public DataTable Consultar()
        {
            try
            {
                variaveis.Clear();
                sql = "select ClienteId, Nome, CPF, DataNascimento, Endereco, Numero, \n" ;
                sql += "Complemento, Bairro, CEP, Telefone, CidadeId, UsuarioId \n";
                sql += "from tblCliente \n";

                if (ClienteId != 0)
                {
                    sql += "where clienteId = @ClienteId";
                    variaveis.Add(new SqlParameter("@ClienteId", ClienteId));
                }
                else if (Nome != string.Empty)
                {
                    sql += "where nome like @nome";
                    variaveis.Add(new SqlParameter("@nome", "%" + Nome + "%"));
                }
                else if (UsuarioId != 0)
                {
                    sql += "where usuarioId = @UsuarioId";
                    variaveis.Add(new SqlParameter("@UsuarioId", UsuarioId));
                }

                dataTable = acessoBD.Consultar(sql, variaveis);
                if (ClienteId != 0)
                {
                    ClienteId = Convert.ToInt32(dataTable.Rows[0]["ClienteId"]);
                    Nome = dataTable.Rows[0]["Nome"].ToString();
                    CPF = dataTable.Rows[0]["CPF"].ToString();
                    DataNascimento = Convert.ToDateTime(dataTable.Rows[0]["DataNascimento"]);
                    Endereco = dataTable.Rows[0]["Endereco"].ToString();
                    Numero = dataTable.Rows[0]["Numero"].ToString();
                    Complemento = dataTable.Rows[0]["Complemento"].ToString();
                    Bairro = dataTable.Rows[0]["Bairro"].ToString();
                    CEP = dataTable.Rows[0]["CEP"].ToString();
                    Telefone = Complemento = dataTable.Rows[0]["Complemento"].ToString();
                    Bairro = dataTable.Rows[0]["Bairro"].ToString();
                    Telefone = dataTable.Rows[0]["Telefone"].ToString();
                    CidadeId = Convert.ToInt32(dataTable.Rows[0]["CidadeId"]);
                    UsuarioId = Convert.ToInt32(dataTable.Rows[0]["UsuarioId"]);


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
                using (TransactionScope Transacao = new TransactionScope()) {
                    if (ClienteId == 0)
                    {
                        sql = "insert into tblCliente (Nome, CPF, DataNascimento, Endereco, Numero, \n ";
                        sql += "Complemento, Bairro, CEP, Telefone, CidadeId, UsuarioId) \n";
                        sql += "values (@Nome, @CPF, @DataNascimento, @Endereco, @Numero, \n";
                        sql += "@Complemento, @Bairro, @CEP, @Telefone, @CidadeId, @UsuarioId) select @@identity \n";
                    }
                    else
                    {
                        sql = "update tblCliente set \n";
                        sql += "Nome= @Nome, \n";
                        sql += "CPF= @CPF, \n";
                        sql += "DataNascimento= @DataNascimento, \n";
                        sql += "Endereco= @Endereco, \n";
                        sql += "Numero= @Numero, \n";
                        sql += "Complemento= @Complemento, \n";
                        sql += "Bairro= @Bairro, \n";
                        sql += "CEP= @CEP, \n";
                        sql += "Telefone= @Telefone, \n";
                        sql += "CidadeId= @CidadeId, \n";
                        sql += "UsuarioId= @UsuarioId \n";
                        sql += "where ClienteId =@ClienteId";
                        variaveis.Add(new SqlParameter("@ClienteId", ClienteId));
                    }
                    variaveis.Add(new SqlParameter("@Nome", Nome));
                    variaveis.Add(new SqlParameter("@CPF", CPF));
                    variaveis.Add(new SqlParameter("@DataNascimento", DataNascimento));
                    variaveis.Add(new SqlParameter("@Endereco", Endereco));
                    variaveis.Add(new SqlParameter("@Numero", Numero));
                    variaveis.Add(new SqlParameter("@Complemento", Complemento));
                    variaveis.Add(new SqlParameter("@Bairro", Bairro));
                    variaveis.Add(new SqlParameter("@CEP", CEP));
                    variaveis.Add(new SqlParameter("@Telefone", Telefone));
                    variaveis.Add(new SqlParameter("@CidadeId", CidadeId));
                    variaveis.Add(new SqlParameter("UsuarioId", UsuarioId));

                    if (ClienteId == 0)
                    {
                        ClienteId = Convert.ToInt32(acessoBD.Executar(variaveis, sql));
                    }
                    else
                    {
                        acessoBD.Executar(sql, variaveis);
                    }
                    Transacao.Complete();
            }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
