using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CASCO.DAO.Contpaqi.Comercial.SDK_2_0_3
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
    public struct tMovimiento
    {
        public int aConsecutivo;
        public double aUnidades;
        public double aPrecio;
        public double aCosto;
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
