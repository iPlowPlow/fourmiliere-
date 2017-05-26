using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier.GestionObjets
{
    class Pheromone : ObjetAbstract
    {
        String nom { get; set; }

        public Pheromone(String nom)
        {
            this.nom = nom;
        }

    }
}
