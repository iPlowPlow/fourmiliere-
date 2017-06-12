using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionStrategie;
using LibAbstraite.GestionEnvironnement;
using LibAbstraite.GestionPersonnage;

namespace LibMetier.GestionStrategie
{
    public class Immobile : StrategieAbstraite
    {
        public Immobile(string nom)
        {
            this.Nom = nom;
        }

        public override void Deplacement(int dimX, int dimY, PersonnageAbstrait unPerso)
        {
           
        }

    }
}
