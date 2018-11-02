using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.EN.Abastos
{
    public class Division: Item
    {
        private List<Empresa> _empresas = new List<Empresa>();
        public List<Empresa> empresas {
            get { return _empresas; }
            set { _empresas = value; }
        }
    }
}
