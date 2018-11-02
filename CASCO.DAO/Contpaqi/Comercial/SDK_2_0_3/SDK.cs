using CASCO.EN.General;
using Microsoft.Win32;
using System;
using System.Text;

namespace CASCO.DAO.Contpaqi.Comercial.SDK_2_0_3
{
	public static class SDK
	{
        static Int32 lResultado;
		public static bool SDK_Iniciado = false;
		public static bool EmpresaAbierta = false;

		public static Int32 Resultado
		{
			get { return lResultado; }
			set { lResultado = value; }
		}

		private static string DirectorioBase
		{
			get
			{
				string s = @"DirectorioBase";
				if (System.Configuration.ConfigurationManager.AppSettings["DirectorioBaseComercial"] != null && System.Configuration.ConfigurationManager.AppSettings["DirectorioBaseComercial"].ToString().Trim().Length > 0)
					s = System.Configuration.ConfigurationManager.AppSettings["DirectorioBaseComercial"].ToString().Trim();
				return s;
			}
		}

		private static string NombrePaq
		{
			get
			{
				string s = @"CONTPAQ I COMERCIAL";
				if (System.Configuration.ConfigurationManager.AppSettings["NombrePaqComercial"] != null && System.Configuration.ConfigurationManager.AppSettings["NombrePaqComercial"].ToString().Trim().Length > 0)
					s = System.Configuration.ConfigurationManager.AppSettings["NombrePaqComercial"].ToString().Trim();
				return s;
			}
		}

		private static bool SetDirectorioBase()
		{
			Resultado = 0;
			try
			{
				RegistryKey keySistema = Registry.LocalMachine.OpenSubKey(DirectorioBase);
				if (keySistema != null)
				{
					object lEntrada = keySistema.GetValue("DirectorioBase");
					System.IO.Directory.SetCurrentDirectory(lEntrada.ToString());
					try
					{
						EventLog.WriteInfo(string.Format("Directorio Base {0}", lEntrada.ToString()));
						Resultado = Funciones.fSetNombrePAQ(NombrePaq);
						if (Resultado != 0)
						{
							rError(Resultado);
							return false;
						}
						EventLog.WriteInfo(string.Format("Se abrió {0} correctamente", NombrePaq));
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
				else
				{
					string msg = "No existe la llave " + DirectorioBase + "en el registro del Sistema";
					EventLog.WriteInfo(msg);
					throw new Exception(msg);
				}
			}
			catch (Exception ex)
			{
				EventLog.WriteError(ex.ToString());
				return false;
			}
			return true;
		}

		public static void rError(Int32 iError)
		{
			StringBuilder sMensaje = new StringBuilder(512);
			if (iError != 0)
			{
				try
				{
					Funciones.fError(iError, sMensaje, 512);
					EventLog.WriteError(sMensaje.ToString());
				}
				catch (Exception ex)
				{
					EventLog.WriteError(ex.ToString());
					TerminaSDK();
				}
			}
		}

		public static void Inicializa()
		{
			SDK_Iniciado = SetDirectorioBase();
			if (!SDK_Iniciado)
				throw new Exception(string.Format("Revisar el log {0} en el visor de eventos del sistema", CASCO.EN.General.Utils.EventSource));
			else
				EventLog.WriteInfo("SDK Inicializado correctamente...");
		}

		public static bool AbreEmpresa(string empresa)
		{
			if (!SDK_Iniciado)
				throw new Exception("No se a iniciado el SDK");

			EmpresaAbierta = false;
			try
			{
				Resultado = Funciones.fAbreEmpresa(empresa);
				if (lResultado != 0)
					rError(lResultado);
				else
				{
					EventLog.WriteInfo(string.Format("Se Abrió {0} Correctamente...", empresa));
					EmpresaAbierta = true;
				}
			}
			catch(Exception ex)
			{
				EventLog.WriteError(ex.ToString());
				TerminaSDK();
			}
			return EmpresaAbierta;
		}

		public static bool CerrarEmpresa(string empresa)
		{
			if (EmpresaAbierta)
				EmpresaAbierta = false;
			try
			{
				Funciones.fCierraEmpresa();
				EventLog.WriteInfo(string.Format("Se Cerró la empresa {0}...", empresa));
				return true;
			}
			catch (Exception ex)
			{
				EventLog.WriteError(ex.ToString());
				TerminaSDK();
			}
			return false;
		}

		public static void CerrarEmpresa()
		{
			EmpresaAbierta = false;
			try
			{
				Funciones.fCierraEmpresa();
				EventLog.WriteInfo("Se Cerró la empresa activa...");
			}
			catch (Exception ex)
			{
				EventLog.WriteError(ex.ToString());
				TerminaSDK();
			}
		}

		public static void TerminaSDK()
		{
			if (SDK_Iniciado)
			{
				if (EmpresaAbierta)
					CerrarEmpresa();
				Funciones.fTerminaSDK();
				SDK_Iniciado = false;
				EventLog.WriteInfo("SDK Finalizado...");
			}
		}

	}
}