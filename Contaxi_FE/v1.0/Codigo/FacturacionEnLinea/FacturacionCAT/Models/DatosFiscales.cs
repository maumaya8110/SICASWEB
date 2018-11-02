using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FacturacionCAT.Models
{
	public class DatosFiscales
	{
		string _alias;
		string _razonSocial;
		string _RFC;
		string _calle;
		string _numeroInterior;
		string _numeroExterior;
		string _colonia;
		string _ciudad;
		string _cp;
		string _localidad;
		string _estado;
		string _pais;
		string _token;
        string _email;
        string _confemail;

		public string Pais { get { return _pais; } set { _pais = value; } }
		public string Token
		{
			get { return _token; }
			set { _token = value; }
		}

		public string Colonia
		{
			get { return _colonia; }
			set { _colonia = value; }
		}

		public string Cp
		{
			get { return _cp; }
			set { _cp = value; }
		}

		public string Ciudad
		{
			get { return _ciudad; }
			set { _ciudad = value; }
		}

		public string Localidad
		{
			get { return _localidad; }
			set { _localidad = value; }
		}

		public string Estado
		{
			get { return _estado; }
			set { _estado = value; }
		}

		public string NumeroExterior
		{
			get { return _numeroExterior; }
			set { _numeroExterior = value; }
		}

		public string NumeroInterior
		{
			get { return _numeroInterior; }
			set { _numeroInterior = value; }
		}

		public string RFC
		{
			get { return _RFC; }
			set { _RFC = value; }
		}

		public string Calle { get { return _calle; } set { _calle = value; } }
		public string RazonSocial
		{
			get { return _razonSocial; }
			set { _razonSocial = value; }
		}

		public string Alias { get { return _alias; } set { _alias = value; } }

        public string Email {
            get { return _email; }
            set { _email = value; }
        }

        public string ConfirmacionEmail {
            get { return _confemail; }
            set { _confemail = value; }
        }

        public static DatosFiscales GetDatosFiscalesBySession(string session)
        {
            DatosFiscales df = null;
            DataTable datos = CoreFacturacion.Negocio.GetDatosFiscalesBySession(session);
            if (datos != null)
            {
                foreach (DataRow dr in datos.Rows)
                {
                    df = new DatosFiscales();
                    df.RazonSocial = dr["Nombre"].ToString();
                    df.Alias = dr["Alias"].ToString();
                    df.Email = dr["CorreoElectronico"].ToString();
                    df.ConfirmacionEmail = dr["CorreoElectronico"].ToString();
                    df.RFC = dr["RFC"].ToString();
                    df.Calle = dr["Calle"].ToString();
                    df.NumeroExterior = dr["NumeroExterior"].ToString();
                    df.NumeroInterior = dr["NumeroInterior"].ToString();
                    df.Colonia = dr["Colonia"].ToString();
                    df.Cp = dr["CodigoPostal"].ToString();
                    df.Ciudad = dr["Ciudad"].ToString();
                    df.Localidad = dr["Localidad"].ToString();
                    df.Estado = dr["Estado"].ToString();
                    df.Token = dr["RazonSocialID"].ToString();
                    df.Pais = "México";
                }
            }
            return df;
        }
    }
}