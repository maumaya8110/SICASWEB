using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CASCO.DAO.Contpaqi.Comercial.SDK_2_0_3
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct tDocumento
    {
        public Double aFolio;
        public int aNumMoneda;
        public Double aTipoCambio;
        public Double aImporte;
        public Double aDescuentoDoc1;
        public Double aDescuentoDoc2;
        public int aSistemaOrigen;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
        public String aCodConcepto;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongSerie)]
        public String aSerie;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongFecha)]
        public String aFecha;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
        public String aCodigoCteProv;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
        public String aCodigoAgente;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongReferencia)]
        public String aReferencia;
        public int aAfecta;
        public int aGasto1;
        public int aGasto2;
        public int aGasto3;
    }
}
