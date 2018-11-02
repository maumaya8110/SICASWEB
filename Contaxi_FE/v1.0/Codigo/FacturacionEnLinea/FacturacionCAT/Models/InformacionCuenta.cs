using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FacturacionCAT.Models
{
	public class InformacionCuenta
	{
		string _nombre;
		string _email;
		string _pwd;

		public string Nombre { get { return _nombre; } set { _nombre = value; } }
		public string Email { get { return _email; } set { _email = value; } }
		public string Pwd { get { return _pwd; } set { _pwd = value; } }
	}
}