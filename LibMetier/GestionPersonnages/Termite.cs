using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionEnvironnement;
using System.Collections.ObjectModel;
using LibAbstraite;
using LibMetier.GestionEnvironnements;

namespace LibMetier.GestionPersonnages
{
    public class Termite : PersonnageAbstrait
    {

        public Termite(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.PV = 75;
            this.Position = position;
            ListEtape = new ObservableCollection<Etape>();
        }
        public override ZoneAbstrait ChoisirZoneSuivante()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "Ma Termite" + this.Nom;
        }

        public override void AnalyseSituation()
        {
            
        }
    }
}
