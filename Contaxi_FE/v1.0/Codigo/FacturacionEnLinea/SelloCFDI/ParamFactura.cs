using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SelloCFDI
{
	public class ParamFactura
	{
		bool _desglozaIVA;

		public bool DesglozaIVA
		{
			get { return _desglozaIVA; }
			set { _desglozaIVA = value; }
		}
		string _regimenFiscal;

		public string RegimenFiscal
		{
			get { return _regimenFiscal; }
			set { _regimenFiscal = value; }
		}
		string _correoElectronicoReceptor;

		public string CorreoElectronicoReceptor
		{
			get { return _correoElectronicoReceptor; }
			set { _correoElectronicoReceptor = value; }
		}
		string _correoElectronicoEmisor;

		public string CorreoElectronicoEmisor
		{
			get { return _correoElectronicoEmisor; }
			set { _correoElectronicoEmisor = value; }
		}
		string _moneda;

		public string Moneda
		{
			get { return _moneda; }
			set { _moneda = value; }
		}
        List<string> _metodoDePago = new List<string>();

		public List<string> MetodoDePago
		{
			get { return _metodoDePago; }
			set { _metodoDePago = value; }
		}
		string _lugarExpedicion;

		public string LugarExpedicion
		{
			get { return _lugarExpedicion; }
			set { _lugarExpedicion = value; }
		}
		string _version;

		public string Version
		{
			get { return _version; }
			set { _version = value; }
		}
		string _numCtaPago;

		public string NumCtaPago
		{
			get { return _numCtaPago; }
			set { _numCtaPago = value; }
		}
		string _formaDePago;

		public string FormaDePago
		{
			get { return _formaDePago; }
			set { _formaDePago = value; }
		}
		string _condicionesDePago;

		public string CondicionesDePago
		{
			get { return _condicionesDePago; }
			set { _condicionesDePago = value; }
		}
		string _serie;

		public string Serie
		{
			get { return _serie; }
			set { _serie = value; }
		}
		string _folio;

		public string Folio
		{
			get { return _folio; }
			set { _folio = value; }
		}
		string _archivoCER;

		public string ArchivoCER
		{
			get { return _archivoCER; }
			set { _archivoCER = value; }
		}
		string _archivoKEY;

		public string ArchivoKEY
		{
			get { return _archivoKEY; }
			set { _archivoKEY = value; }
		}
		string _contrasena;

		public string Contrasena
		{
			get { return _contrasena; }
			set { _contrasena = value; }
		}
		string _WSURL;

		public string WSURL
		{
			get { return _WSURL; }
			set { _WSURL = value; }
		}
		string _WSUsuario;

		public string WSUsuario
		{
			get { return _WSUsuario; }
			set { _WSUsuario = value; }
		}
		string _WSContrasena;

		public string WSContrasena
		{
			get { return _WSContrasena; }
			set { _WSContrasena = value; }
		}
		string _WSArchivoCER;

		public string WSArchivoCER
		{
			get { return _WSArchivoCER; }
			set { _WSArchivoCER = value; }
		}
		string _WSArchivoCERContrasena;

		public string WSArchivoCERContrasena
		{
			get { return _WSArchivoCERContrasena; }
			set { _WSArchivoCERContrasena = value; }
		}
		string _pathFacturas;

		public string PathFacturas
		{
			get { return _pathFacturas; }
			set { _pathFacturas = value; }
		}
		string _txtConcepto;

		public string TxtConcepto
		{
			get { return _txtConcepto; }
			set { _txtConcepto = value; }
		}
		string _token;

		public string Token
		{
			get { return _token; }
			set { _token = value; }
		}
		string _wsurlCancelacion;

		public string WsurlCancelacion
		{
			get { return _wsurlCancelacion; }
			set { _wsurlCancelacion = value; }
		}
		string _unidad;

		public string Unidad
		{
			get { return _unidad; }
			set { _unidad = value; }
		}
		string _urlImagen;

		public string UrlImagen
		{
			get { return _urlImagen; }
			set { _urlImagen = value; }
		}
		double _TasaIVA;

		public double TasaIVA
		{
			get { return _TasaIVA; }
			set { _TasaIVA = value; }
		}


	}
}