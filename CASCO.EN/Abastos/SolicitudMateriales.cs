using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASCO.EN.Abastos
{
    public class SolicitudMateriales : ICloneable
    {
        public int? id { get; set; }
        public string FechaElabora { get; set; }
        private Item _departamento = new Item();
        private Item _empresa = new Item();
        private Item _division = new Item();
        private Item _almacen = new Item();
        private Item _status = new Item();

        public string FolioDocumento { get; set; }
        public string SerieDocumento { get; set; }

        public Item Departamento { get { return _departamento; } set { _departamento = value; } }

        public Item Empresa { get { return _empresa; } set { _empresa = value; } }

        public Item Division { get { return _division; } set { _division = value; } }

        public Item Almacen { get { return _almacen; } set { _almacen = value; } }

        public string Usuario { get; set; }

        public string BaseDeDatos { get; set; }

        public string ConceptoDocumento { get; set; }

        public string Moneda { get; set; }

        public Item Estatus { get { return _status; } set { _status = value; } }

        private List<DetalleSolicitudMateriales> _articulos = new List<DetalleSolicitudMateriales>(); 

        public List<DetalleSolicitudMateriales> articulos { get { return _articulos; } set { _articulos = value; } }

        private List<SoporteElectronicoSolicitudMateriales> _soportes = new List<SoporteElectronicoSolicitudMateriales>();

        public List<SoporteElectronicoSolicitudMateriales> soportes { get { return _soportes; } set { _soportes = value; } }

        public double total {
            get
            {
                double t = 0;
                foreach(DetalleSolicitudMateriales a in _articulos)
                {
                    t += a.monto;
                }
                return t;
            }
        }

        public string Comentarios { get; set; }

        public int CIDProveedor {
            get {
                int s=0;
                if (articulos.Count > 0)
                    s = articulos[0].item.CIDPROVEEDOR;
                return s; }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    
}