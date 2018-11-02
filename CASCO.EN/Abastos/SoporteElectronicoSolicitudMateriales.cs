using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.EN.Abastos
{
    public class SoporteElectronicoSolicitudMateriales :Item
    {
        public int Orden { get; set; }
        public int Solicitud_Id { get; set; }

        public int Estatus { get; set; }

        public string Ruta_Documento { get; set; }

        private SoporteElectronico _soporte = new SoporteElectronico();
        public SoporteElectronico soporte { get { return _soporte; } set { _soporte = value; } }
        
    }
}
