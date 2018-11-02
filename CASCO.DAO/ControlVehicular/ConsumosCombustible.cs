using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CASCO.EN.ControlVehicular;
using System.Collections;
using System.Data;

namespace CASCO.DAO.ControlVehicular
{
    public static class ConsumosCombustible
    {
        public static List<Unidad> GetUnidadesConConsumo()
        {
            List<Unidad> lunidades = new List<Unidad>();
            string sqlstr = "dbo.usp_Orsan_ObtieneUnidadesConConsumo";
            Hashtable m_params = new Hashtable();
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                Unidad u = new Unidad();
                u.id = Convert.ToInt32(dr["ID"]);
                u.descripcion = dr["Descripcion"].ToString();
                u.Unidad_ID = Convert.ToInt32(dr["ID"]);
                u.ModeloUnidad_ID = Convert.ToInt32(dr["ID"]);
                u.Modelo = dr["Modelo"].ToString();
                u.aux = u.Unidad_ID.ToString();
                lunidades.Add(u);
            }

            return lunidades;
        }

        public static List<ConsumoCombustible> GetConsumosPorFecha(int unidad, DateTime dteIni, DateTime dteFin)
        {
            List<ConsumoCombustible> lconsumos = new List<ConsumoCombustible>();
            string sqlstr = "dbo.usp_Orsan_ObtieneConsumos";
            Hashtable m_params = new Hashtable();
            m_params.Add("@NoVehiculo", unidad);
            m_params.Add("@FechaInicial", dteIni);
            m_params.Add("@FechaFinal", dteFin);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                ConsumoCombustible cc = new ConsumoCombustible();
                cc.id = Convert.ToInt32(dr["ConsumoCombustible_ID"]);
                cc.descripcion = dr["Unidad"].ToString();
                cc.NoVehiculo = Convert.ToInt32(dr["NoVehiculo"]);
                cc.Conductor = dr["Conductor"].ToString();
                cc.FechaInicial = Convert.ToDateTime(dr["FechaInicial"]);
                cc.FechaFinal = Convert.ToDateTime(dr["FechaFinal"]);
                cc.Litros = Convert.ToDouble(dr["Litros"]);
                cc.Pesos = Convert.ToDouble(dr["Pesos"]);
                cc.KmInicial = Convert.ToInt32(dr["KmInicial"]);
                cc.KmFinal = Convert.ToInt32(dr["KmFinal"]);
                cc.KmRecorridos = Convert.ToInt32(dr["KmRecorridos"]);
                cc.Rendimiento = Convert.ToDouble(dr["Rendimiento"]);
                cc.Servicios = Convert.ToInt32(dr["Servicios"]);
                cc.Ingresos = Convert.ToDouble(dr["Ingresos"]);
                cc.ModeloUnidad_ID = Convert.ToInt32(dr["ModeloUnidad_ID"]);
                cc.Modelo = dr["Modelo"].ToString();
                cc.aux = cc.KmRecorridos.ToString();
                lconsumos.Add(cc);
            }

            return lconsumos;
        }
    }
}
