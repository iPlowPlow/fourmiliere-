using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier.GestionEnvironnements
{
    class Chemin : AccesAbstrait
    {
        public ZoneAbstraite zDebut;
        public ZoneAbstraite zFin;
        public Chemin(ZoneAbstraite zDebut, ZoneAbstraite zFin)
        {
            this.zDebut = zDebut;
            this.zFin = zFin;
        }

    }
}
