using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FacturacionCAT.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public Estado(int id, string descripcion)
        {
            Id = id;
            Descripcion = descripcion;
        }

        public static List<Estado> GetEstados()
        {
            List<Estado> lEstados = new List<Estado>();
            DataTable dt = CoreFacturacion.SQLHelper.GetTableWithName("Estados", "FF_ConsultaEstados", null);
            foreach(DataRow dr in dt.Rows)
            {
                lEstados.Add(new Estado(Convert.ToInt32(dr[0]), dr[1].ToString()));
            }
            return lEstados;
        }
    }
}