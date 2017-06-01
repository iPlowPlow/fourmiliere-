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
    public class Ouvriere : PersonnageAbstrait
    {
        public Ouvriere(String nom)
        {
            PV = 2;
            position = new Coordonnees();
            position.X = 10;
            position.Y = 10;
            ListEtape = new ObservableCollection<Etape>();
            this.Nom = nom;
        }
        public override string ToString()
        {
            return "Mon Ouvriere" + this.Nom;
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
