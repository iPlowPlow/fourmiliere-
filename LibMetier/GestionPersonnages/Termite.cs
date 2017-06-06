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

        public Termite(String nom, CoordonneesAbstrait position)
        {
            this.nom = _nom;
            this.Pointsdevie = 75;
            this.Position = position;
            ListEtape = new ObservableCollection<Etape>();
        }
        public override ZoneAbstrait ChoisirZoneSuivante()
        {
            throw new NotImplementedException();
        }
        public override void Avance1Tour(int dimX, int dimY)
        {
            AvanceAuHazard(dimX, dimY);
            //ListEtape.Add(new Etape());
        }
        public override void AvanceAuHazard(int dimX, int dimY)
        {
            int newX = Position.X + Hazard.Next(3) - 1;
            int newY = Position.Y + Hazard.Next(3) - 1;
            if ((newX >= 0) && (newX < dimX)) Position.X = newX;
            if ((newY >= 0) && (newY < dimX)) Position.Y = newY;
        }

        public override string ToString()
        {
            return "Ma Termite" + this.Nom;
        }

        public override void AnalyseSituation()
        {
            throw new NotImplementedException();
        }
    }
}
