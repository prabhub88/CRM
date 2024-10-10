using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
namespace DAL
{
    public class SqlDataHelper : ISqlDataHelper
    {
        string connectionString = string.Empty;
        private static SqlConnection conn = null;
        private static SqlCommand cmd = null;
        public SqlDataHelper(IConfiguration _config)
        {
            connectionString =_config["AppSettings:DbConnectionString"];
        }
        public bool InsertOrUpdate(string SP_Name, params SqlParameter[] Parameter_Values)
        {
            int R = 0;
            bool Result = false;
            try
            {
                using (conn = GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = SP_Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (SqlParameter p in Parameter_Values)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }

                        cmd.Parameters.Add(p);
                    }
                    R = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    if (R > 0)
                        Result = true;

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return Result;

        }

        public DataSet GetDataset(string SP_Name, params SqlParameter[] Parameter_Values)
        {
            DataSet ds = new DataSet();
            try
            {
                using (conn = GetConnection())
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = SP_Name;
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (SqlParameter p in Parameter_Values)
                    {
                        if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }

                        cmd.Parameters.Add(p);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    cmd.Parameters.Clear();
                }
            }
            
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
                if (cmd != null)
                {
                    cmd.Dispose();
                }
            }
            return ds;
        }

        private  SqlConnection GetConnection()
        {
            return String.IsNullOrEmpty(connectionString)
                ? throw new ArgumentNullException("Connection string not declared")
                : new SqlConnection(connectionString);
        }
    }
}
