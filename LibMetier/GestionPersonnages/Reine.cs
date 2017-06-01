using LibAbstraite.GestionPersonnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;

namespace LibMetier.GestionPersonnages
{
  public  class Reine : PersonnageAbstrait 
    {
        public Reine()
        {
           
        }


        public Reine(string nom)
        {
            this.Nom = nom;
            this.pointsdevie = 500;
        }

        public override ZoneAbstrait ChoixZoneSuivante()
        {
            throw new NotImplementedException();
        }
    }
}
