using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionEnvironnement;
using System.Collections.ObjectModel;
using LibAbstraite;

namespace LibMetier.GestionPersonnages
{
    public class Termite : PersonnageAbstrait
    {

        public Termite(String nom)
        {
            PV = 75;
            X = 10;
            Y = 10;
            ListEtape = new ObservableCollection<Etape>();
            this.Nom = nom;
        }

        public override string ToString()
        {
            return "Ma Termite" + this.Nom;
        }


        public override ZoneAbstrait ChoixZoneSuivante(List<AccesAbstrait> accesList)
        {
            throw new NotImplementedException();
        }

        public override void AnalyseSituation()
        {
            throw new NotImplementedException();
        }
    }
}
