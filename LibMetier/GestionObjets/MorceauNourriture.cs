using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionObjets;


namespace LibMetier.GestionObjets
{
    public class MorceauNourriture : ObjetAbstrait
    {
        public MorceauNourriture()
        {

        }
        public MorceauNourriture(string nom)
        {
            this.Nom = nom;
        }
        public override void TourPasse()
        {

        }
    }
}
