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

        public override string ToString()
        {
            return "Ma Reine" + this.Nom;
        }

        public Reine(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.Pointsdevie = 500;
            this.Position = position;
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

        public override void AnalyseSituation()
        {
            throw new NotImplementedException();
        }
    }
}
