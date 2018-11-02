using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CASCO.DAO.Contabilidad
{
    public static class CorteCaja
    {
        public static List<EN.Contabilidad.CorteCaja> GetCortesCaja(string usuario, DateTime inicio, DateTime fin, int empresa_id, int caja_id)
        {
            List<EN.Contabilidad.CorteCaja> lcc = new List<EN.Contabilidad.CorteCaja>();
            string sqlstr = "[dbo].[usp_CortesCaja_ObtieneEstatusTraslado]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@Usuario_ID", usuario);
            m_params.Add("@FechaInicio", inicio);
            m_params.Add("@FechaFin", fin);
            m_params.Add("@Empresa_ID", empresa_id);
            m_params.Add("@Caja_ID", caja_id);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                EN.Contabilidad.CorteCaja c = new EN.Contabilidad.CorteCaja();
                c.id = Convert.ToInt32(dr["CorteCaja_ID"]);
                c.aux = dr["CorteCaja_ID"].ToString();

                c.Sesion.id = Convert.ToInt32(dr["Sesion_ID"].ToString());
                c.FechaInicial = Convert.ToDateTime(dr["FechaInicial"].ToString());
                c.FechaFinal = Convert.ToDateTime(dr["FechaFinal"].ToString());
                c.FechaCorte = Convert.ToDateTime(dr["FechaDeCorte"].ToString());
                c.Empresa.id = Convert.ToInt32(dr["Empresa_ID"]);
                c.Empresa.descripcion = dr["Empresa"].ToString();
                c.Empresa.aux = c.Empresa.id.ToString();
                c.Estacion.id = Convert.ToInt32(dr["Estacion_ID"]);
                c.Estacion.descripcion = dr["Estacion"].ToString();
                c.Estacion.aux = c.Estacion.id.ToString();
                c.Caja.id = Convert.ToInt32(dr["Caja_ID"]);
                c.Caja.descripcion = dr["Caja"].ToString();
                c.Caja.aux = c.Caja.id.ToString();
                c.Estatus.id = Convert.ToInt32(dr["EstatusCorteCaja_ID"]);
                c.Estatus.descripcion = dr["EstatusCorteCaja"].ToString();
                c.Estatus.aux = c.Estatus.id.ToString();
                c.TotalIngresosEfectivo = Convert.ToDouble(dr["TotalIngresosEfectivo"]);
                c.Observaciones = dr["Observaciones"].ToString();
                c.EstatusTraslado.id = Convert.ToInt32(dr["EstatusTrasladoCorteCaja_ID"]);
                c.EstatusTraslado.Abreviacion = dr["EstatusTraslado_Abr"].ToString();
                c.EstatusTraslado.Columna = dr["EstatusTraslado_Col"].ToString();
                c.EstatusTraslado.descripcion = dr["EstatusTraslado"].ToString();
                c.A = Convert.ToBoolean(dr["A"]);
                c.B = Convert.ToBoolean(dr["B"]);
                c.C = Convert.ToBoolean(dr["C"]);

                lcc.Add(c);
            }
            return lcc;
        }
    }
}
