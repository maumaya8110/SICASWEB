using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace CASCO.DAO
{
    public class DBL
    {

        public static int ExecuteNonQuerySP(Hashtable mparams, string sp_name, string conexion, int timeOut)
        {
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand(sp_name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            cmd.Parameters.Clear();
            int i = 0;
            foreach (object k in mparams.Keys)
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = k.ToString();
                param.Value = (mparams[k] != null) ? mparams[k] : null;
                cmd.Parameters.Add(param);
            }
            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = "Error al ejecutar el SP " + sp_name;
                throw new Exception(msg, ex);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return i;
        }

        public static DataSet ExecuteSelectSP(Hashtable mparams, string sp_name, string conexion, int timeOut)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand(sp_name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = timeOut;
            cmd.Parameters.Clear();
            foreach (object k in mparams.Keys)
            {
                SqlParameter param = new SqlParameter();
                param.ParameterName = k.ToString();
                param.Value = (mparams[k] != null) ? mparams[k] : null;
                cmd.Parameters.Add(param);
            }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                string msg = "Error al ejecutar el SP " + sp_name;
                throw new Exception(msg, ex);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public static CellSet ExecuteOLAP(string mdx, string conexion, int timeout)
        {
            AdomdConnection con = new AdomdConnection(conexion);
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al conectarse al Origen de Datos.", ex);
            }

            AdomdCommand command = con.CreateCommand();
            command.CommandText = mdx;
            command.CommandTimeout = timeout;
            command.CommandType = CommandType.Text;
            CellSet cs = null;

            try
            {
                cs = command.ExecuteCellSet();
            }
            catch
            {
                throw new Exception("Ocurrió un error al Ejecutar la consulta MDX");
            }

            return cs;
        }

    }
}
