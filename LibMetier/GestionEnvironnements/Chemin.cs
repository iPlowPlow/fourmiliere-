using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;
namespace LibMetier.GestionEnvironnements
{
    class Chemin : AccesAbstrait
    {
 
        public Chemin(List<ZoneAbstrait> zoneaccess)
        {
            zonesaccessibles = zoneaccess;
        }

    }
}
