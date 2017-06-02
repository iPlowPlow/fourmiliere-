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
    public class Reine : PersonnageAbstrait 
    {

        public Reine(String nom)
        {
            PV = 100;
            position = new Coordonnees();
            position.X = 10;
            position.Y = 10;
            ListEtape = new ObservableCollection<Etape>();
            this.Nom = nom;
        }

        public override string ToString()
        {
            return "Ma Reine" + this.Nom;
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
