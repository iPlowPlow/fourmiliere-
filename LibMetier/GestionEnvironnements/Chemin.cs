using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;
namespace LibMetier.GestionEnvironnements
{
    public class Chemin : AccesAbstrait
    {
 
        public Chemin(ZoneAbstrait zFin, ZoneAbstrait zDebut)
        {
       
            this.Debut = zDebut;
            this.Fin = zFin;
        }

    }
}
