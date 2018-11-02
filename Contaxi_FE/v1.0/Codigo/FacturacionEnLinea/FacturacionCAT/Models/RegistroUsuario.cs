using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionCAT.Models
{
	public class RegistroUsuario
	{
		public bool Valido { get; set; }
		public InformacionCuenta InfoCuenta { get; set; }
		public List<DatosFiscales> RazonesSociales { get; set; }
	}
}