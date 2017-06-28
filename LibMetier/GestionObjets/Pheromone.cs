using LibAbstraite.GestionObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;
using LibAbstraite;

namespace LibMetier.GestionObjets
{
    public class Pheromone : ObjetAbstrait
    {
        public static int DureeVieOriginale = 25;
        //décrémenté à chaque tour
       
        public Pheromone()
        {
        
        }
        public Pheromone(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.Position = position;
            this.Dureevie = DureeVieOriginale;
        }

        public override void TourPasse(SujetAbstrait meteo)
        {
            if(meteo.Etat == "pluie" && Dureevie > 5)
            {
                Dureevie -= 4;
            }
            Dureevie --;
        }

    }
}
