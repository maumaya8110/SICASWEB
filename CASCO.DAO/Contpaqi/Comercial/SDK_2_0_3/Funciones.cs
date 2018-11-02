using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Text;

namespace CASCO.DAO.Contpaqi.Comercial.SDK_2_0_3
{
	public static class Funciones
	{
		#region General
		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fSetNombrePAQ([MarshalAs(UnmanagedType.LPStr)] string aNombrePAQ);

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fAbreEmpresa([MarshalAs(UnmanagedType.LPStr)] string Directorio);

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern void fCierraEmpresa();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern void fTerminaSDK();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fInicializaSDK();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern void fError(Int32 NumeroError, [MarshalAs(UnmanagedType.LPStr)] StringBuilder Mensaje, int Longitud);

		#endregion

		#region Documentos
		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fAltaDocumento(ref Int32 aIdDocumento, ref tDocumento aDocumento);

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fAltaDocumentoCargoAbono(ref tDocumento aDocumento);

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fGuardaDocumento();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fBorraDocumento();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fEditarDocumento();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fCancelarModificacionDocumento();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fSetDatoDocumento([MarshalAs(UnmanagedType.LPStr)] string aCampo, [MarshalAs(UnmanagedType.LPStr)] StringBuilder aValor);

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fSiguienteFolio([MarshalAs(UnmanagedType.LPStr)] string aCodigoConcepto, [MarshalAs(UnmanagedType.LPStr)] StringBuilder aSerie, ref double aFolio);

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fBuscarDocumento([MarshalAs(UnmanagedType.LPStr)] string aCodConcepto, [MarshalAs(UnmanagedType.LPStr)] string aSerie, [MarshalAs(UnmanagedType.LPStr)] string aFolio);

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fBuscarIdDocumento(int aIdDocumento);

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fBuscaDocumento(ref tRegLlaveMov aLlaveDocto);

		#endregion

		#region Proveedores

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fPosPrimerCteProv();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fPosUltimoCteProv();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fPosSiguienteCteProv();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fPosAnteriorCteProv();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fLeeDatoCteProv([MarshalAs(UnmanagedType.LPStr)] string aCampo, [MarshalAs(UnmanagedType.LPStr)] StringBuilder aValor, int aLen);

        [DllImport("MGWSERVICIOS.DLL", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		public static extern Int32 fBuscaCteProv([MarshalAs(UnmanagedType.LPStr)] string aCodConcepto);

		[DllImport("MGWSERVICIOS.DLL", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		public static extern Int32 fBuscaIdCteProv(int aIdCteProv);

		[DllImport("MGWSERVICIOS.DLL", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		public static extern Int32 fAltaCteProv(ref Int32 aIdCteProv, ref tCteProv astCteProv);

		#endregion

		#region Conceptos

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fPosPrimerConceptoDocto();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fPosUltimaConceptoDocto();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fPosSiguienteConceptoDocto();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fPosAnteriorConceptoDocto();

		[DllImport("MGWSERVICIOS.DLL", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		public static extern Int32 fLeeDatoConceptoDocto([MarshalAs(UnmanagedType.LPStr)] string aCampo, [MarshalAs(UnmanagedType.LPStr)] StringBuilder aValor, int aLen);

		[DllImport("MGWSERVICIOS.DLL", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
		public static extern Int32 fBuscaConceptoDocto([MarshalAs(UnmanagedType.LPStr)] string aCodConcepto);

		#endregion

		#region Movimientos

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fEditarMovimiento();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fGuardaMovimiento();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fCancelaCambiosMovimiento();

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fAltaMovimientoCaracteristicas_Param([MarshalAs(UnmanagedType.LPStr)] string aIdMovimiento, [MarshalAs(UnmanagedType.LPStr)] string aIdMovtoCaracteristicas, [MarshalAs(UnmanagedType.LPStr)] string aUnidades, [MarshalAs(UnmanagedType.LPStr)] string aValorCaracteristica1, [MarshalAs(UnmanagedType.LPStr)] string aValorCaracteristica2, [MarshalAs(UnmanagedType.LPStr)] string aValorCaracteristica3);

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fSetDatoMovimiento([MarshalAs(UnmanagedType.LPStr)] string aCampo, [MarshalAs(UnmanagedType.LPStr)] StringBuilder Valor);

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fLeeDatoMovimiento([MarshalAs(UnmanagedType.LPStr)] string aCampo, [MarshalAs(UnmanagedType.LPStr)] StringBuilder aValor, int aLen);

		[DllImport("MGWSERVICIOS.DLL")]
		public static extern Int32 fAltaMovimiento(ref Int32 aIdDocumento, ref Int32 aIdMovimiento, ref tMovimiento astMovimiento);

		#endregion
	}
}
