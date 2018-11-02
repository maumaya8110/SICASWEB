using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using CASCO.EN.EquipoDeGas.Inventarios;

namespace CASCO.DAO.EquipoDeGas
{
    public static class Inventarios
    {
        public static List<Almacen> ObtieneAlmacenesPorUsuario(string usuario_id)
        {
            List<Almacen> lAlmacenes = new List<Almacen>();
            string sqlstr = "[dbo].[usp_Inventarios_Bodegas_Consulta]";
            Hashtable m_params = new Hashtable();

            m_params.Add("@Usuario_ID", usuario_id);
            DataTable dt = DBL.ExecuteSelectSP(m_params, sqlstr, Utils.CadenaConexionSICAS, Utils.ConexionTimeOut).Tables[0];

            return GetAlmacenesList(dt);
        }

        private static List<Almacen> GetAlmacenesList(DataTable dt)
        {
            List<Almacen> lAlmacenes = new List<Almacen>();
            foreach (DataRow dr in dt.Rows)
            {
                Almacen a = new Almacen();
                a.id = Convert.ToInt32(dr["Bodega_ID"]);
                a.descripcion = dr["Nombre"].ToString();
                a.selected = dr["selected"].ToString();

                lAlmacenes.Add(a);
            }
            return lAlmacenes;
        }
    }
}
