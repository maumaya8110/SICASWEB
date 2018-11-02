using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security;
using System.Xml.XPath;
using System.IO;

namespace SelloCFDI
{
	public static class Sello
	{
		public static string CrearSelloDigitalEmisor(byte[] cadena, ParamFactura pFactura)
		{
			try
			{
				string ArchivoCertificado = pFactura.ArchivoCER;
				string key = pFactura.ArchivoKEY;
				string lPassword = pFactura.Contrasena;
				SecureString identidad = new SecureString();// Se requerira un objeto SecureString que represente el password de la clave privada, que se obtiene asi:
				identidad.Clear();
				foreach (char c in lPassword.ToCharArray())
				{
					identidad.AppendChar(c);
				}
				Byte[] pLlavePrivadaenBytes = System.IO.File.ReadAllBytes(key);// Se lee el archivo .key
				RSACryptoServiceProvider lrsa = opensslkey.DecodeEncryptedPrivateKeyInfo(pLlavePrivadaenBytes, identidad);// Uso de la clase opensslkey
				SHA1CryptoServiceProvider hasher = new SHA1CryptoServiceProvider();
				Byte[] bytesFirmados = lrsa.SignData(cadena, hasher);
				string sellodigital = Convert.ToBase64String(bytesFirmados);// Obtengo Sello
				return sellodigital;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
