using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LibAbstraite.GestionStrategie;

namespace LibAbstraite.GestionPersonnage
{

    public abstract class PersonnageAbstrait : ViewModelBase
    {
        public StrategieAbstraite StategieCourante { get; set; }
        public bool TransporteNourriture = false;
        
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
        public CoordonneesAbstrait Maison { get; set; }

        public abstract ZoneAbstrait ChoisirZoneSuivante();
        
        public  ObservableCollection<Etape> ListEtape { get; set; }

        public abstract void AnalyseSituation();

        public void Avance1Tour(int dimX, int dimY, int tourActuel)
        {
            AnalyseSituation();
            this.StategieCourante.Deplacement(dimX, dimY, this);
            ListEtape.Add(new Etape(tourActuel, "Position X : " + Position.X +", Y : " + Position.Y));
 
            if (PV > 0) { PV--; }
            
        }

       
    }
}
