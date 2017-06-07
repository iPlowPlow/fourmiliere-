using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;
namespace LibMetier.GestionEnvironnements
{
    public class Acces : AccesAbstrait
    {
 
        public Acces(List<ZoneAbstrait> zoneaccess)
        {
            Zonesaccessibles = zoneaccess;
        }

    }
}
