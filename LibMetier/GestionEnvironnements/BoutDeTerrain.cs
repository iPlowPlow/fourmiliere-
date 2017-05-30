using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier.GestionEnvironnements
{
    class BoutDeTerrain : ZoneAbstraite
    {
        public String nom { get; set; }

        public BoutDeTerrain(String nom)
        {
            this.nom = nom;
        }

    }
}
