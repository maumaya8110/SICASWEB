using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CASCO.DAO.Contpaqi.Comercial.SDK_2_0_3
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct tMovimientoDesc
    {
        public int aConsecutivo;
        public double aUnidades;
        public double aPrecio;
        public double aCosto;
        public double aPorcDescto1;
        public double aImporteDescto1;
        public double aPorcDescto2;
        public double aImporteDescto2;
        public double aPorcDescto3;
        public double aImporteDescto3;
        public double aPorcDescto4;
        public double aImporteDescto4;
        public double aPorcDescto5;
        public double aImporteDescto5;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
        public string aCodProdSer;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
        public string aCodAlmacen;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongReferencia)]
        public string aReferencia;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = constantes.kLongCodigo)]
        public string aCodClasificacion;
    }
}
