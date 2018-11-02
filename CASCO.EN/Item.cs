using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CASCO.EN
{
    public class Item
    {
        public int? id { get; set; }
        public string descripcion { get; set; }

        public string selected { get; set; }

        public string aux { get; set; }

        public Item()
        {

        }
    }
}