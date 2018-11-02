using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.EN.Contabilidad
{
    public class ComprasPorPagar
    {
        public int CIDDOCUMENTO { get; set; }
        public string CSERIEDOCUMENTO { get; set; }
        public string CFOLIO { get; set; }
        public DateTime CFECHA { get; set; }
        public string CRAZONSOCIAL { get; set; }
        public DateTime CFECHAVENCIMIENTO { get; set; }
        public DateTime CFECHAENTREGARECEPCION { get; set; }
        public int CIDMONEDA { get; set; }
        public double CNETO { get; set; }
        public double CIMPUESTO1 { get; set; }
        public double CTOTAL { get; set; }

        public double CPAGOPARCIALIDAD { get; set; }
        public double CPENDIENTE { get; set; }
        public string CTEXTOEXTRA1 { get; set; }
        public string CBANOBSERVACIONES { get; set; }
        public int CAFECTADO { get; set; }
        public bool CIMPRESO { get; set; }
        public int CCANCELADO { get; set; }
        public int CDEVUELTO { get; set; }
        public int CIDPREPOLIZA { get; set; }
        public int CIDPREPOLIZACANCELACION { get; set; }
        public int CESTADOCONTABLE { get; set; }

        public string Estatus { get; set; }
        public int CIDCONCEPTODOCUMENTO { get; set; }
        public string CNOMBRECONCEPTO { get; set; }
        public string BaseDeDatos { get; set; }
        public int CIDPROVEEDOR { get; set; }

        public DateTime FechaProgPago { get; set; }
        public DateTime FechaPago { get; set; }

        public int CDIASCREDITO { get; set; }

        private Abastos.Empresa _empresa = new Abastos.Empresa();
        public Abastos.Empresa empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public string Empresa
        {
            get
            { return this.empresa.id > 0 ? this.empresa.descripcion : "N/A"; }
        }

        public string CREFERENCIA { get; set; }


        public DateTime FechaEntregaDoctos { get; set; }


        public int DiasVencimiento { get; set; }
        public DateTime FechaEntregaDocumentos { get; set; }
    }
}
