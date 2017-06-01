using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionEnvironnement;

namespace LibMetier.GestionPersonnages
{
  public  class Termite : PersonnageAbstrait
    {
        String nom { get; set; }
        public Termite(String _nom)
        {
            this.nom = _nom;
            this.pointsdevie = 30;
        }
        public override ZoneAbstrait ChoixZoneSuivante()
        {
            throw new NotImplementedException();
        }
    }
}
