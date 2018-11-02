using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CASCO.EN.Abastos
{
    public class Usuario : Item
    {
        private List<Division> _ldivision = new List<Division>();
        public List<Division> divisiones { get { return _ldivision; } set { _ldivision = value; } }

        public int NivelAcceso
        {
            get
            {
                int inivel = 10;
                foreach (Division d in _ldivision)
                {
                    foreach(Empresa e in d.empresas)
                    {
                        foreach(Departamento depto in e.departamentos)
                        {
                            if (depto.NivelAcceso < inivel)
                                inivel = depto.NivelAcceso;
                        }
                    }
                }
                return inivel;
            }
        }
    }
}
