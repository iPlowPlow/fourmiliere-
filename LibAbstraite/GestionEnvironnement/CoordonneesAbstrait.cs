using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibAbstraite.GestionEnvironnement
{
    public abstract class CoordonneesAbstrait
    {
  
        public int X { get; set; }

        public int Y { get; set; }

        public abstract String toString();
    }
}
