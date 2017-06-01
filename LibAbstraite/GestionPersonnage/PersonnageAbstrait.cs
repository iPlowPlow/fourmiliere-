
using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibAbstraite.GestionPersonnage
{
    public abstract class PersonnageAbstrait : ViewModelBase
    {
        static Random Hazard = new Random();
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

        public abstract ZoneAbstrait ChoixZoneSuivante(List<AccesAbstrait> accesList);
        public CoordonneesAbstrait position
        {
            get; set;
        }

        public  ObservableCollection<Etape> ListEtape { get; set; }

        public abstract void AnalyseSituation();

        public void Avance1Tour(int dimX, int dimY, int tourActuel)
        { 
            AvanceAuHazard(dimX, dimY);
            ListEtape.Add(new Etape(tourActuel, "position X : " + position.X +" Position Y : " + position.Y));
            //AnalyseSituation();
            if (PV > 0) { PV--; }
            
        }

        public void AvanceAuHazard(int dimX, int dimY)
        {
            int newX = position.X + Hazard.Next(3) - 1;
            int newY = position.Y + Hazard.Next(3) - 1;
            if ((newX >= 0) && (newX < dimX)) position.X = newX;
            if ((newY >= 0) && (newY < dimX)) position.Y = newY;
        }

 

    }
}
