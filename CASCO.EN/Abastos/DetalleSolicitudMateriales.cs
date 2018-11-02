using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASCO.EN.Abastos
{
    public class DetalleSolicitudMateriales
    {
        public int id { get; set; }
        private ItemDetalle _item = new ItemDetalle();

        public string FolioDocumento { get; set; }
        public string SerieDocumento { get; set; }

        public int CIDDOCUMENTO { get; set; }

        public int CIDMOVIMIENTO { get; set; }

        public ItemDetalle item
        {
            get { return _item; }
            set { _item = value; }
        }
        public int cantidad { get; set; }
        public double monto { get { return cantidad * item.preciocompra; } }

    }

    public class ItemDetalle : Item
    {
        public int CIDPRODUCTO { get; set; }
        public int CIDPROVEEDOR { get; set; }
        public int CCODIGOPRODUCTO { get; set; }

        public double preciocompra { get; set; }

        public string BaseDeDatos { get; set; }

    }
}