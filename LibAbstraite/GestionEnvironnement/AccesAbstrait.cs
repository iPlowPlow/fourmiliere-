using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite.GestionEnvironnement
{
    public abstract class AccesAbstrait
    {
        ZoneAbstrait Debut { get; set; }
        ZoneAbstrait Fin { get; set; }
        public AccesAbstrait(ZoneAbstrait zdebut,ZoneAbstrait zfin)
        {
            this.Debut = zdebut;
            this.Fin = zfin;
        }
    }
}
