using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibAbstraite.GestionEnvironnement;
using System.Threading.Tasks;
using LibAbstraite.GestionObjets;
using LibAbstraite;

namespace LibMetier.GestionObjets
{
    public class MorceauNourriture : ObjetAbstrait
    {
        public MorceauNourriture()
        {

        }
        public MorceauNourriture(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.Position = position;
        }
        public override void TourPasse(SujetAbstrait meteo)
        {

        }
    }
}
