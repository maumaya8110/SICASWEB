using CASCO.EN.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.DAO.Contpaqi.Comercial.SDK_2_0_3
{
	public sealed class Documento
	{
		private Int32 _idDocto;

        private Int32 lResultado;
        public Int32 Resultado
        {
            get { return lResultado; }
            set { lResultado = value; }
        }
        public Int32 Id { get { return _idDocto; } }
		private string _folio;
		private string _serie;
		public string Folio { get { return _folio; } }

		public Documento() { }

		public bool AltaDeDocumento(tDocumento docto)
		{
			if (!SDK.EmpresaAbierta)
				throw new Exception("Primero debe establecer una empresa");
			Int32 idDocto = 0;
			Resultado = Funciones.fAltaDocumento(ref idDocto, ref docto);
			if (Resultado == 0)
			{
				_idDocto = idDocto;
				EventLog.WriteInfo(string.Format("Se Creó el Documento {0} Correctamente...", idDocto));
				return true;
			}
            SDK.rError(Resultado);
			return false;
		}

		public string ObtieneFolioSiguiente(string codConcepto)
		{
            if(!SDK.SDK_Iniciado)
                throw new Exception("Primero debe inicializar el SDK");
            if (!SDK.EmpresaAbierta)
				throw new Exception("Primero debe abrir una empresa");
			double f = 0;
			StringBuilder s = new StringBuilder();
			Resultado = Funciones.fSiguienteFolio(codConcepto, s, ref f);
			if (Resultado == 0)
			{
				_folio = f.ToString();
				_serie = s.ToString();
				EventLog.WriteInfo(string.Format("Se obtuvo el Folio siguiente: {0}", _folio));
				return _folio;
			}
            SDK.rError(Resultado);
			return "";
		}

		public string ObtieneCodigoConcepto(string concepto)
		{
            if (!SDK.SDK_Iniciado)
                throw new Exception("Primero debe inicializar el SDK");

            if (!SDK.EmpresaAbierta)
				throw new Exception("Primero debe abrir una empresa");

			StringBuilder codConcepto = new StringBuilder();
			StringBuilder nombreConcepto = new StringBuilder();

			string codaux = "";
			Resultado = Funciones.fPosPrimerConceptoDocto();
			while (Resultado == 0 && codaux.Trim().Length == 0)
			{
				try
				{
					Resultado = Funciones.fLeeDatoConceptoDocto("CCODIGOCONCEPTO", codConcepto, 30);
				}
				catch (Exception ex)
				{
					EventLog.WriteError(ex.ToString());
					Resultado = -1;
				}
				if (Resultado == 0)
				{
					try
					{
						Resultado = Funciones.fLeeDatoConceptoDocto("CNOMBRECONCEPTO", nombreConcepto, 60);
					}
					catch (Exception ex)
					{
						EventLog.WriteError(ex.ToString());
						Resultado = -1;
					}
					if (Resultado == 0)
					{
						if (concepto == nombreConcepto.ToString())
							codaux = codConcepto.ToString();
					}
				}

				if (Resultado == 0 && codaux.Trim().Length == 0)
					Resultado = Funciones.fPosSiguienteConceptoDocto();
			}

			if (Resultado == 0 && codaux.Trim().Length > 0)
				EventLog.WriteInfo(string.Format("Se Obtuvo el codigo de concepto {0} para {1}", codaux, concepto));
			else if (Resultado > 0)
				SDK.rError(Resultado);

			return codaux;
		}

        public string ObtieneInfoOrdenCompra(string concepto)
        {
            if (!SDK.SDK_Iniciado)
                throw new Exception("Primero debe inicializar el SDK");

            if (!SDK.EmpresaAbierta)
                throw new Exception("Primero debe abrir una empresa");

            StringBuilder lNombreC = new StringBuilder();
            Resultado = Funciones.fBuscaConceptoDocto(concepto);
            if (Resultado == 0)
                Resultado = Funciones.fLeeDatoConceptoDocto("CNOMBRECONCEPTO", lNombreC, 60);

            if (Resultado == 0)
                EventLog.WriteInfo(string.Format("Se Obtuvo la descripcion del concepto {0} : {1}", concepto, lNombreC));

            SDK.rError(Resultado);
            return lNombreC.ToString();
        }

		public bool SetDatoDocumento(string llave, StringBuilder valor)
		{
            if (!SDK.SDK_Iniciado)
                throw new Exception("Primero debe inicializar el SDK");

            if (!SDK.EmpresaAbierta)
				throw new Exception("Primero debe abrir una empresa");
			
			Resultado = Funciones.fEditarDocumento();
			Resultado = Funciones.fSetDatoDocumento(llave, valor);
			Resultado = Funciones.fGuardaDocumento();
			SDK.rError(Resultado);
			return Resultado == 0;
		}

		public Int32 AltaMovimiento(tMovimiento tm)
		{
            if (!SDK.SDK_Iniciado)
                throw new Exception("Primero debe inicializar el SDK");

            if (!SDK.EmpresaAbierta)
				throw new Exception("Primero debe establecer una empresa");

			Int32 idMvto = 0;
			Resultado = Funciones.fAltaMovimiento(ref _idDocto, ref idMvto, ref tm);
			if (Resultado == 0)
				EventLog.WriteInfo(string.Format("Se Creó el Movimiento {0} Correctamente...", idMvto.ToString()));
            SDK.rError(Resultado);
			return idMvto;
		}

		public bool SetDatoMovimiento(string llave, StringBuilder valor)
		{
            if (!SDK.SDK_Iniciado)
                throw new Exception("Primero debe inicializar el SDK");

            if (!SDK.EmpresaAbierta)
				throw new Exception("Primero debe abrir una empresa");

			Resultado = Funciones.fEditarMovimiento();
			Resultado = Funciones.fSetDatoMovimiento(llave, valor);
			Resultado = Funciones.fGuardaMovimiento();
            SDK.rError(Resultado);
			return Resultado == 0;
		}

        public string BuscaProveedorPorId(int cIDPROVEEDOR)
        {
            if (!SDK.SDK_Iniciado)
                throw new Exception("Primero debe inicializar el SDK");

            if (!SDK.EmpresaAbierta)
                throw new Exception("Primero debe abrir una empresa");

            Resultado = Funciones.fBuscaIdCteProv(cIDPROVEEDOR);
            StringBuilder sCodProveedor = new StringBuilder();
            if (Resultado == 0)
                Resultado = Funciones.fLeeDatoCteProv("CCODIGOCLIENTE", sCodProveedor, constantes.kLongCodigo);

            if (Resultado == 0)
                EventLog.WriteInfo(string.Format("Se Obtuvo el Código del Proveedor {0} : {1}", cIDPROVEEDOR, sCodProveedor));

            SDK.rError(Resultado);
            return sCodProveedor.ToString();
        }

        public int ObtieneIdMoneda(string moneda)
        {
            return (moneda == "MXN") ? 1 : 2;
        }

    }
}