using CASCO.EN.EquipoDeGas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CASCO.DAO.EquipoDeGas
{
    public static class EquiposGas
    {
        public static List<EN.EquipoDeGas.Master> ObtieneMaster()
        {
            List<EN.EquipoDeGas.Master> lcp = new List<EN.EquipoDeGas.Master>();
            string sqlstr = "[DBO].[usp_EquiposGas_Master_Consulta]";
            Hashtable m_params = new Hashtable();
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                EN.EquipoDeGas.Master cp = new EN.EquipoDeGas.Master();

                cp.id = Convert.ToInt32(dr["MasterEquipoGas_ID"]);
                if (!DB.IsNullOrEmpty(dr["Empresa"]))
                    cp.Empresa = dr["Empresa"].ToString();

                if (!DB.IsNullOrEmpty(dr["Estacion"]))
                    cp.Estacion = dr["Estacion"].ToString();

                if (!DB.IsNullOrEmpty(dr["NumeroEconomico"]))
                    cp.NumeroEconomico = dr["NumeroEconomico"].ToString();

                if (!DB.IsNullOrEmpty(dr["Gas"]))
                    cp.Gas = dr["Gas"].ToString();

                if (!DB.IsNullOrEmpty(dr["Serie_Regulador"]))
                    cp.SerieRegulador= dr["Serie_Regulador"].ToString();

                if (!DB.IsNullOrEmpty(dr["Serie_Cilindro"]))
                    cp.SerieCilindro = dr["Serie_Cilindro"].ToString();

                if (!DB.IsNullOrEmpty(dr["Conductor"]))
                    cp.Conductor = dr["Conductor"].ToString();

                if (!DB.IsNullOrEmpty(dr["Locacion"]))
                    cp.Locacion = dr["Locacion"].ToString();

                if (!DB.IsNullOrEmpty(dr["Modelo"]))
                    cp.Modelo = dr["Modelo"].ToString();

                if (!DB.IsNullOrEmpty(dr["Placas"]))
                    cp.Placas = dr["Placas"].ToString();

                if (!DB.IsNullOrEmpty(dr["NumeroSerie"]))
                    cp.NumeroSerie = dr["NumeroSerie"].ToString();
                lcp.Add(cp);
            }
            return lcp;
        }

        public static object InsertaRegistroMaster(RegistroMaster registro, string usuario_id)
        {
            string sql = "[dbo].[usp_EquiposGas_Master_Inserta]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@MasterEquipoGas_ID", registro.ID);
            m_params.Add("@Serie_Regulador", registro.SerieRegulador);
            m_params.Add("@Serie_Cilindro", registro.SerieCilindro);
            m_params.Add("@NumeroEconomico", registro.NumeroEconomico);
            m_params.Add("@Usuario_ID", usuario_id);
            object obj = DB.ExecuteCommandSP_With_Return(sql, m_params);
            return Convert.ToBoolean(obj);
        }
    }
}
