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
   public class Guerriere : PersonnageAbstrait
    {

        public Guerriere()
        {
         
        }
        public Guerriere(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
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
            int newX = this.Position.X + Hazard.Next(3) - 1;
            int newY = this.Position.Y + Hazard.Next(3) - 1;
            if ((newX >= 0) && (newX < dimX)) this.Position.X = newX;
            if ((newY >= 0) && (newY < dimX)) this.Position.Y = newY;
        }

        public override string ToString()
        {
            return "Ma Guerriere" + this.Nom;
        }

        public override void AnalyseSituation()
        {
            throw new NotImplementedException();
        }

    }
}
