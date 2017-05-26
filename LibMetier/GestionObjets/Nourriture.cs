using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier.GestionObjets
{
    class Nourriture : ObjetAbstract
    {
        String nom { get; set; }

        public Nourriture(String nom)
        {
            this.nom = nom;
        }

    }
}
