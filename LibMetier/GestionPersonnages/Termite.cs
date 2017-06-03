using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using LibAbstraite.GestionEnvironnement;

namespace LibMetier.GestionPersonnages
{
    public  class Termite : PersonnageAbstrait
    {
        String nom { get; set; }
        public Termite(String _nom)
        {
            this.nom = _nom;
            this.Pointsdevie = 30;
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
    }
}
