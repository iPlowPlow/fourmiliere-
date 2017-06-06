using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LibAbstraite.GestionPersonnage
{

    public abstract class PersonnageAbstrait : ViewModelBase
    {
        public bool TransporteNourriture = false;
        public static Random Hazard = new Random();
        public AccesAbstrait ChoixZoneSuivante { get; set; }
        public string Nom { get; set; }
        public int pv {get; set;}
        public int PV
        {
            get { return pv; }
            set
            {
                pv = value;
                OnPropertyChanged("PV");
            }
        }
        public ZoneAbstrait zone;

        public CoordonneesAbstrait Position { get; set; }
        
        public abstract ZoneAbstrait ChoisirZoneSuivante();
        
        public  ObservableCollection<Etape> ListEtape { get; set; }

        public abstract void AnalyseSituation();

        public void Avance1Tour(int dimX, int dimY, int tourActuel)
        { 
            AvanceAuHazard(dimX, dimY);
            ListEtape.Add(new Etape(tourActuel, "D�placement � la position X : " + Position.X +", Y : " + Position.Y));
            AnalyseSituation();
            if (PV > 0) { PV--; }
            
        }

        public void AvanceAuHazard(int dimX, int dimY)
        {
            int newX = Position.X + Hazard.Next(3) - 1;
            int newY = Position.Y + Hazard.Next(3) - 1;
            if ((newX >= 0) && (newX < dimX)) Position.X = newX;
            if ((newY >= 0) && (newY < dimX)) Position.Y = newY;
        }
    }
}
