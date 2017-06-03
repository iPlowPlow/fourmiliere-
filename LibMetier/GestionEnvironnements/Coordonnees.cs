using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;

namespace LibMetier.GestionEnvironnements
{

    public class Coordonnees : CoordonneesAbstrait
    {
        public Coordonnees(int positionx, int positiony)
        {
            X = positionx;
            Y = positiony;
        }
        public override String toString()
        {
            return X.ToString() + "," + Y.ToString(); 
        }
    }
}
