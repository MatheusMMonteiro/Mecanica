using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace ProjetoMecanico
{
    public class AcessoBD
    {

        SqlConnection connection;

        private void Conectar()
        {
            connection = new SqlConnection(Global.StringConexao);

            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Desconectar()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable Consultar(string sql, List<SqlParameter> variaveis)
        {
            try
            {
                Conectar();
                SqlCommand command = new SqlCommand(sql, connection);
                foreach (SqlParameter variavel in variaveis)
                {
                    command.Parameters.Add(variavel);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }

        public void Executar(string sql, List<SqlParameter> variaveis)
        {
            try
            {
                Conectar();
                SqlCommand command = new SqlCommand(sql, connection);
                foreach (SqlParameter variavel in variaveis)
                {
                    command.Parameters.Add(variavel);
                }

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }

        public string Executar(List<SqlParameter> variaveis, string sql)
        {
            try
            {
                Conectar();
                SqlCommand command = new SqlCommand(sql, connection);
                foreach (SqlParameter variavel in variaveis)
                {
                    command.Parameters.Add(variavel);
                }


                return command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }


    }
}
