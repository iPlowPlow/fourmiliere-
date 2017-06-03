using LibAbstraite.GestionObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;

namespace LibMetier.GestionObjets
{
    public class Pheromone : ObjetAbstrait
    {
        //décrémenté à chaque tour
        public int dureevie;
        public Pheromone()
        {
        
        }
        public Pheromone(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.position = position;
            dureevie = 25;
        }

        public void TourPasse()
        {
            dureevie --;
        }

    }
}
