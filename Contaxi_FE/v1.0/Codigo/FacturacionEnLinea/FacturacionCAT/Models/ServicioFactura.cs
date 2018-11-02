using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionCAT.Models
{
	public class ServicioFactura
	{
		public string Folio { get; set; }
		public string Fecha { get; set; }
		public string Producto { get; set; }
		public string Unidad { get; set; }
		public double Precio { get; set; }
		public double Importe { get; set; }
        public string MetodoPago { get; set; }
        public string CuentaBanco { get; set; }
	}
}