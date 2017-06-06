using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionEnvironnement;
using LibMetier.GestionObjets;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using LibAbstraite;
using LibMetier.GestionEnvironnements;

namespace LibMetier.GestionPersonnages
{
    
  public class Ouvriere : PersonnageAbstrait
   {
        public MorceauNourriture Morceau { get; set; }
        public Ouvriere()
        {
         
        }
        public Ouvriere(string nom, CoordonneesAbstrait position)
        {
            this.Nom = nom;
            this.Pointsdevie = 20;
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
        public MorceauNourriture DeposeMorceau()
        {
            MorceauNourriture morceauRendu = Morceau;
            Morceau = null;
            return morceauRendu;
        }
        public override string ToString()
        {
            return "Mon Ouvriere" + this.Nom;
        }

        public override void AnalyseSituation()
        {
            throw new NotImplementedException();
        }
    }
}
