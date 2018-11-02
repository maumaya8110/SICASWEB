using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.EN.General
{
    public class Permisos
    {
        Dictionary<string, bool> _permisos = new Dictionary<string, bool>();

        public string Usuario { get; set; }

        public Dictionary<string, bool> LPermisos {
            get { return _permisos; }
            set { _permisos = value; }
        }
    }
}
