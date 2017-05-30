using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier.GestionObjets
{
    class Oeuf : ObjetAbstract
    {
        String nom { get; set; }
        public Oeuf(String nom)
        {
            this.nom = nom;
        }

    }
}
