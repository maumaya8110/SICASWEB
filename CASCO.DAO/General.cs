using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CASCO.EN.Contabilidad;
using System.Collections;
using System.Data;

namespace CASCO.DAO
{
    public static class General
    {
        public static List<EN.General.Empresa> GetEmpresasUsuario(string usuario)
        {
            List<EN.General.Empresa> lemp = new List<EN.General.Empresa>();
            string sqlstr = "[dbo].[usp_web_General_Obtiene_EmpresasUsuario]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@Usuario_ID", usuario);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

           foreach(DataRow dr in dt.Rows)
            {
                EN.General.Empresa e = new EN.General.Empresa();
                e.id = Convert.ToInt32(dr["Empresa_ID"]);
                e.descripcion = dr["Descripcion"].ToString();
                e.aux = e.id.ToString();
                lemp.Add(e);
            }
            return lemp;
        }

        public static List<EN.General.Caja> GetCajasUsuario(string usuario)
        {
            List<EN.General.Caja> lcajas = new List<EN.General.Caja>();
            string sqlstr = "[dbo].[usp_web_General_Obtiene_CajasUsuario]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@Usuario_ID", usuario);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                EN.General.Caja c = new EN.General.Caja();
                c.id = Convert.ToInt32(dr["Caja_ID"]);
                c.descripcion = dr["Descripcion"].ToString();
                c.aux = dr["Empresa_ID"].ToString();
                lcajas.Add(c);
            }
            return lcajas;
        }

        public static EN.General.Permisos GetPermisosUsuario(string usuario)
        {
            EN.General.Permisos p = new EN.General.Permisos();
            p.Usuario = usuario;
            string sqlstr = "[contabilidad].[usp_web_General_Obtiene_PermisosUsuario]";
            Hashtable m_params = new Hashtable();
            m_params.Add("@Usuario_ID", usuario);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                p.LPermisos.Add(dr[0].ToString(),Convert.ToBoolean(dr[1]));
            }
            return p;
        }
    }
}
