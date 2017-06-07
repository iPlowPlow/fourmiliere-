using LibAbstraite.GestionPersonnage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionEnvironnement;
using System.Collections.ObjectModel;
using LibAbstraite;
using LibMetier.GestionEnvironnements;

namespace LibMetier.GestionPersonnages
{
  public  class Reine : PersonnageAbstrait 
    {
        public Reine()
        {
           
        }

        public Reine(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.PV = 500;
            this.Position = position;
            ListEtape = new ObservableCollection<Etape>();
        }




        public override ZoneAbstrait ChoisirZoneSuivante()
        {
            throw new NotImplementedException();
        }

        public override void AnalyseSituation()
        {

        }


        public override string ToString()
        {
            return "Ma Reine" + this.Nom;
        }
    }
}
