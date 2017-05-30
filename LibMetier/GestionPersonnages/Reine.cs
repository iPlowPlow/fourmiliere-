using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMetier.GestionPersonnages
{
    class Reine : PersonnageAbstrait 
    {
        String nom { get; set; }
        public Reine(String nom)
        {
            this.nom = nom;
        }

    }
}
