using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.EN.Abastos
{
    public class SolicitudMaterialesConsulta : SolicitudMateriales
    {
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }
}
