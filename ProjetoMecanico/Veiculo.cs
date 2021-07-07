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
    public class Veiculo
    {
        //tblVeiculo
        public int VeiculoId { get; set; }
        public string Placa { get; set; }
        public string AnoFabricacao { get; set; }
        public string Cor { get; set; }
        public int CombutivelId { get; set; }
        public int ClienteId { get; set; }
        //tblmodelo
        public string Modelo { get; set; }
        public int ModeloId { get; set; }
        //tblMarca
        public string Marca { get; set; }
        public int MarcaId { get; set; }

        public Veiculo()
        {
            VeiculoId = 0;
            Placa = string.Empty;
            AnoFabricacao = string.Empty;
            Cor = string.Empty;
            CombutivelId = 0;
            ClienteId = 0;
            Modelo = string.Empty;
            ModeloId = 0;
            Marca = string.Empty;
            MarcaId = 0;
        }
        AcessoBD acessoBD = new AcessoBD();
        DataTable dataTable = new DataTable();
        List<SqlParameter> variaveis = new List<SqlParameter>();
        string sql = string.Empty;

        public DataTable ConsultarVeiculo()
        {
            try
            {
                variaveis.Clear();
                sql = "select VeiculoId, Placa, AnoFabricacao, Cor, CombustivelId, modeloId, ClienteId \n";
                sql += "from tblVeiculo \n";
                if(ClienteId!=0)
                sql += "where ClienteId = @ClienteId";
                
                variaveis.Add(new SqlParameter("@ClienteId", ClienteId));


                dataTable = acessoBD.Consultar(sql, variaveis);
                if (ClienteId != 0)
                {
                    VeiculoId = Convert.ToInt32(dataTable.Rows[0]["VeiculoId"]);
                    Placa = dataTable.Rows[0]["Placa"].ToString();
                    AnoFabricacao = dataTable.Rows[0]["AnoFabricacao"].ToString();
                    Cor = dataTable.Rows[0]["Cor"].ToString();
                    CombutivelId = Convert.ToInt32(dataTable.Rows[0]["CombustivelId"]);
                    ModeloId = Convert.ToInt32(dataTable.Rows[0]["ModeloId"]);
                    ClienteId = Convert.ToInt32(dataTable.Rows[0]["ClienteId"]);
                }
                
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return dataTable;
        }
        public DataTable ConsultarModelo()
        {
            try
            {
                variaveis.Clear();
                sql = "select modeloId, modelo, marcaId \n";
                sql += "from tblmodelo \n";
                if (ModeloId != 0)
                sql += "where modeloId = @ModeloId";

                variaveis.Add(new SqlParameter("@ModeloId", ModeloId));

                dataTable = acessoBD.Consultar(sql, variaveis);
                if (ModeloId != 0)
                {
                    ModeloId = Convert.ToInt32(dataTable.Rows[0]["ModeloId"]);
                    Modelo = dataTable.Rows[0]["Modelo"].ToString();
                    MarcaId = Convert.ToInt32(dataTable.Rows[0]["MarcaId"]);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return dataTable;
        }
        public DataTable ConsultarMarca()
        {
            try
            {
                variaveis.Clear();
                sql = "select marcaId, marca \n";
                sql += "from tblmarca \n";
                if(MarcaId!=0)
                sql += "where marcaId = @MarcaId";

                variaveis.Add(new SqlParameter("@MarcaId", MarcaId));
                dataTable = acessoBD.Consultar(sql, variaveis);
                if (MarcaId != 0)
                {
                    MarcaId = Convert.ToInt32(dataTable.Rows[0]["MarcaId"]);
                    Marca = dataTable.Rows[0]["Marca"].ToString();
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return dataTable;
        }


    }
}
