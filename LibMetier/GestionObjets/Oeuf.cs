using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionObjets;
namespace LibMetier.GestionObjets
{
  public  class Oeuf : ObjetAbstrait
    {

        public Oeuf()
        {
          
        }
        public Oeuf(string nom)
        {
            this.Nom = nom;
        }

    }
}
