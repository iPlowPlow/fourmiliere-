using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;

namespace LibMetier.GestionEnvironnements
{
    class Coordonnees : CoordonneesAbstrait
    {
        public override String toString()
        {
            return X.ToString() + "," + Y.ToString(); 
        }
    }
}
