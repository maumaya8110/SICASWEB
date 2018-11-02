
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.EN.Abastos
{
    public class SoporteElectronico:Item
    {
        public int Orden { get; set; }

        public bool Req_Para_Autorizacion { get; set; }

        public int Estatus { get; set; }

        private TipoSoporteElectronico _tipo = new TipoSoporteElectronico();

        public TipoSoporteElectronico Tipo { get { return _tipo; } set { _tipo = value; } }
    }
}
