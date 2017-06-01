using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionEnvironnement;

namespace LibMetier.GestionPersonnages
{
   public class Guerriere : PersonnageAbstrait
    {

        public Guerriere()
        {
         
        }
        public Guerriere(string nom)
        {
            this.Nom = nom;
        }
        public override ZoneAbstrait ChoixZoneSuivante(List<AccesAbstrait> accesList)
        {
            throw new NotImplementedException();
        }
    }
}
