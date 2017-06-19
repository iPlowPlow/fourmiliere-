using LibAbstraite.GestionEnvironnement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LibAbstraite.GestionStrategie;
using System.Threading;
using System.Windows.Threading;

namespace LibAbstraite.GestionPersonnage
{

    public abstract class PersonnageAbstrait : ViewModelBase,Observable
    {
        public StrategieAbstraite StategieCourante { get; set; }


        public string EtatMeteoObserver { get; set; }
        public string EtatFourmiliereObserver { get; set; }
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
        
        public  ObservableCollection<EtapeAbstraite> ListEtape { get; set; }

        public abstract void AnalyseSituation();


        public abstract void AjouterEtape(int tourActuel, string description, int X, int Y);
       

        public void Avance1Tour(int dimX, int dimY, int tourActuel)
        {
            AjouterEtape(tourActuel, "Position X : " + Position.X + ", Y : " + Position.Y, Position.X, Position.Y);
            AnalyseSituation();
            this.StategieCourante.Deplacement(dimX, dimY, this); 
            if (PV > 0) { PV--; } 
        }


        public abstract void maj(string etat);
      
    }
}
