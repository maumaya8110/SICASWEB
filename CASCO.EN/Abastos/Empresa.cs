using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.EN.Abastos
{
    public class Empresa :Item
    {
        private List<Departamento> _departamentos = new List<Departamento>();
        public List<Departamento> departamentos {
            get { return _departamentos; }
            set { _departamentos = value; }
        }

        public string ADD { get; set; }
    }
}
