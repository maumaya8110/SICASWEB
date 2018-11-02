using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.EN.Abastos
{
    public class Proveedor : Item
    {
        public string CIDPROVEEDOR {
            get; set;
        }

        public string CCODIGOPROVEEDOR { get; set; }

        public int CDIASCREDITO { get; set; }

        private Empresa _empresa = new Empresa();
        public Empresa empresa { get { return _empresa; } set { _empresa = value; } }
    }
}
