using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASCO.EN.Abastos
{
    public class ArticuloSolicitudMateriales: Item
    {
        public int SERVICIO_ID { get; set; }
        public string CIDPRODUCTO { get; set; }
        public string CIDPROVEEDOR { get; set; }
        public string CODIGOPRODUCTO { get; set; }
        public string CNOMBREPRODUCTO { get; set; }
        public double preciocompra { get; set; }
        public int KILOMETRAJE_MILES { get; set; }

        public static List<ArticuloSolicitudMateriales> GetArticulosPorProveedor(string proveedor)
        {
            List<ArticuloSolicitudMateriales> articulos = new List<ArticuloSolicitudMateriales>();

            return articulos;
        }
    }
}