using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite.GestionEnvironnement
{
    public abstract class AccesAbstrait
    {
        public ZoneAbstrait Debut { get; set; }
        public ZoneAbstrait Fin { get; set; }
      
    }
}
