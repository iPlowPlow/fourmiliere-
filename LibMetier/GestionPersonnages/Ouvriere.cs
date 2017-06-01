using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionEnvironnement;
using System.Xml.Serialization;

namespace LibMetier.GestionPersonnages
{
    
  public class Ouvriere : PersonnageAbstrait
   {

        public Ouvriere()
        {
         
        }
        public Ouvriere(string nom)
        {
            this.Nom = nom;
            this.pointsdevie = 20;
        }
        public override ZoneAbstrait ChoixZoneSuivante()
        {
            throw new NotImplementedException();
        }
    }
}
