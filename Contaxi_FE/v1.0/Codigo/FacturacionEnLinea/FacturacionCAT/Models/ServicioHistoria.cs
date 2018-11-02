using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionCAT.Models
{
	public class ServicioHistoria
	{
		public string TokenFactura { get; set; }
		public string UsuarioID { get; set; }
		public string Emisor_RFC { get; set; }
		public string Receptor_RFC { get; set; }
		public string Serie { get; set; }
		public string Folio { get; set; }
		public DateTime FechaFactura { get; set; }
		public double Total { get; set; }
		public string UUID { get; set; }
		public DateTime FechaTimbrado { get; set; }
		public string ArchivoXML { get; set; }
		public string ArchivoPDF { get; set; }
		public string ConError { get; set; }
		public string DetalleError { get; set; }
		public string RazonSocialID { get; set; }
		public string Cliente { get; set; }
		public string Tickets { get; set; }
	}
}