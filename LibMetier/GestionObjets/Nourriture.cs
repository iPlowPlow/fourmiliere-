using LibAbstraite.GestionObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier.GestionObjets
{
   public class Nourriture : ObjetAbstrait
    {

        public Nourriture()
        {
         
        }
        public Nourriture(string nom)
        {
            this.Nom = nom;
        }

    }
}
