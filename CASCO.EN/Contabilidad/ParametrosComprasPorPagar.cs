using System.Collections.Generic;

namespace CASCO.EN.Contabilidad
{
    public class ParametrosComprasPorPagar
    {
        public string ciddocumento { get; set; }

        public string valor { get; set; }

        private List<Abastos.Empresa> _empresa = new List<Abastos.Empresa>();

        public List<Abastos.Empresa> empresa { get { return _empresa; } set { _empresa = value; } }

        private List<Abastos.Proveedor> _proveedor = new List<Abastos.Proveedor>();

        public List<Abastos.Proveedor> proveedor { get { return _proveedor; } set { _proveedor = value; } }

        private List<EN.Item> _tiposproveedor = new List<EN.Item>();

        public List<EN.Item> tiposproveedor { get { return _tiposproveedor; } set { _tiposproveedor = value; } }

        public string fechainicio { get; set; }

        public string fechafin { get; set; }

        public List<Item> _estatus { get; set; }
        public List<Item> estatus { get { return _estatus; } set { _estatus = value; } }

        public int tipoProveedor { get; set; }

        public string FechaProgPago { get; set; }

        public string FechaEntregaDocto { get; set; }

        public string FechaPago { get; set; }

        public double MontoTotal { get; set; }

        public double MontoProgramado { get; set; }

        public double MontoPagado { get; set; }

        public double SaldoPendiente { get; set; }

        public bool Todas { get; set; }
    }
}