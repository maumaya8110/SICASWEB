using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.EN.Contabilidad
{
    public class CorteCaja:Item
    {
        private EN.General.Sesion _sesion = new General.Sesion();
        public EN.General.Sesion Sesion { get { return _sesion; } set { _sesion = value; } }

        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public DateTime FechaCorte { get; set; }

        private EN.General.Empresa _empresa = new General.Empresa();
        public EN.General.Empresa Empresa { get { return _empresa; } set { _empresa = value; } }

        private EN.General.Estacion _estacion = new General.Estacion();
        public EN.General.Estacion Estacion { get { return _estacion; } set { _estacion = value; } }

        private EN.General.Caja _caja = new General.Caja();
        public EN.General.Caja Caja { get { return _caja; } set { _caja = value; } }

        private EstatusCorteCaja _estatus = new EstatusCorteCaja();
        public EstatusCorteCaja Estatus { get { return _estatus; } set { _estatus = value; } }

        public double TotalIngresosEfectivo { get; set; }
        public string Observaciones { get; set; }
        private EstatusTrasladoCorteCaja _estatustraslado = new EstatusTrasladoCorteCaja();
        public EstatusTrasladoCorteCaja EstatusTraslado { get { return _estatustraslado; } set { _estatustraslado = value; } }

        public bool A { get; set; }
        public bool B { get; set; }
        public bool C { get; set; }
    }
}
