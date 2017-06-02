using LibAbstraite.GestionObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier.GestionObjets
{
   public class Pheromone : ObjetAbstrait
    {

        public Pheromone()
        {
       
        }
        public Pheromone(string nom)
        {
            this.Nom = nom;
        }

    }
}
