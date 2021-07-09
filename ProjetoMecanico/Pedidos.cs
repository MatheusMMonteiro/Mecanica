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
    public class Pedidos
    {
        public int PedidoId { get; set; }
        public DateTime DataPedido { get; set; }
        public string Ocorrencia { get; set; }
        public byte Imagem { get; set; }
        public string Resposta { get; set; }
        public int VeiculoId { get; set; }
        public int SituacaoId { get; set; }
        public int ClienteId { get; set; }

        public Pedidos()
        {
            PedidoId = 0;
            DataPedido = Convert.ToDateTime("01/07/2021");
            Ocorrencia = string.Empty;
            
            Resposta = string.Empty;
            VeiculoId = 0;
            SituacaoId = 0;
            ClienteId = 0;
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
                sql = "select PedidoId, DataPedido, Ocorrencia, Imagem, Resposta, VeiculoId, SituacaoId, ClienteId \n";
                sql += "from tblPedido \n";

                if (PedidoId != 0)
                {
                    sql += "where pedidoId = @PedidoId";
                    variaveis.Add(new SqlParameter("@PedidoId", PedidoId));
                }
                else if (SituacaoId != 0)
                {
                    sql += "where situacaoId = @SituacaoId";
                    variaveis.Add(new SqlParameter("@SituacaoId", SituacaoId));
                }

                dataTable = acessoBD.Consultar(sql, variaveis);
                if (PedidoId != 0)
                {
                    PedidoId = Convert.ToInt32(dataTable.Rows[0]["PedidoId"]);
                    DataPedido = Convert.ToDateTime(dataTable.Rows[0]["DataPedido"]);
                    Ocorrencia = dataTable.Rows[0]["Ocorrencia"].ToString();
                    //Imagem = Convert.ToByte(dataTable.Rows[0]["Imagem"]);
                    Resposta = dataTable.Rows[0]["Resposta"].ToString();
                    VeiculoId = Convert.ToInt32(dataTable.Rows[0]["VeiculoId"]);
                    SituacaoId = Convert.ToInt32(dataTable.Rows[0]["SituacaoId"]);
                    ClienteId = Convert.ToInt32(dataTable.Rows[0]["ClienteId"]);

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dataTable;                          
        }
        public void ResponderOcorrencia()
        {
            try
            {
               
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public void Gravar()
        {
            try
            {
                variaveis.Clear();
                using (TransactionScope Transacao = new TransactionScope())
                {
                    if (PedidoId == 0)
                    {
                        sql = "insert into tblPedido (DataPedido, Ocorrencia, Resposta, VeiculoId, SituacaoId) \n";
                        sql += "values(@Data, @Ocorrencia, @Resposta, @VeiculoId, @SituacaoId) select @@identity \n";
                    }
                    else
                    {
                        sql = "update tblPedido set \n";
                        sql += "DataPedido = @DataPedido,";
                        sql += "Ocorrencia = @Ocorrencia,";
                       // sql += "Imagem = @Imagem,";
                        sql += "Resposta = @Resposta,";
                        sql +="VeiculoId = @VeiculoId,";
                        sql += "SituacaoId = @SituacaoId \n";
                        sql += "where PedidoId = @PedidoId";
                        variaveis.Add(new SqlParameter("@PedidoId", PedidoId));
                    }
                    variaveis.Add(new SqlParameter("@DataPedido", DataPedido));
                    variaveis.Add(new SqlParameter("@Ocorrencia", Ocorrencia));
                   // variaveis.Add(new SqlParameter("@Imagem", Imagem));
                    variaveis.Add(new SqlParameter("@Resposta",Resposta));
                    variaveis.Add(new SqlParameter("@VeiculoId",VeiculoId));
                    variaveis.Add(new SqlParameter("@SituacaoId", SituacaoId));

                    if (PedidoId == 0)
                    {
                        PedidoId = Convert.ToInt32(acessoBD.Executar(variaveis, sql));
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
