using LibAbstraite.Fabrique;
using LibAbstraite.GestionObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibAbstraite.GestionPersonnage;
using System.Collections.ObjectModel;
using System.Threading;

namespace LibAbstraite.GestionEnvironnement
{
    public abstract class EnvironnementAbstrait : ViewModelBase

    {

        
        public static Random Hazard = new Random();
        public static FabriqueAbstraite Fabrique;
        public Boolean encours;
        public SujetAbstrait meteo { get; set; }
     
        public string Etat { get; set; }
        private string titre;
        private int tourActuel = 1;
        public int TourActuel
        {
            get { return this.tourActuel; }
            set
            {
                this.tourActuel = value;
                OnPropertyChanged("TourActuel");
            }
        }
        public int vitesse;
        private PersonnageAbstrait unPerso;
        public PersonnageAbstrait PersoSelect
        {
            get { return unPerso; }
            set
            {
                unPerso = value;
                OnPropertyChanged("PersoSelect");
            }
        }
        public string TitreApplication
        {
            get { return titre; }
            set
            {
                titre = value;
                OnPropertyChanged("TitreApplication");
            }
        }

        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
        public List<AccesAbstrait> AccesList { get; set; }
        public ObservableCollection<ObjetAbstrait> ObjetList { get; set; }
        public ObservableCollection<PersonnageAbstrait> PersonnagesList { get; set; }
        public List<PersonnageAbstrait> ListObservateur { get; set; }
        public ObservableCollection<PersonnageAbstrait> PersonnagesMortList { get; set; }
        public ObservableCollection<ZoneAbstrait> ZoneList { get; set; }
        public abstract void AjouteOeuf(ObjetAbstrait unObject);
        public abstract void AjoutePheromone(ObjetAbstrait unObject);
        public abstract void AjouterReine();
        public abstract void AjouterGuerriere();
        public abstract void AjouterPrincesse();
        public abstract void AjouterOuvriere();
        public abstract void AjouterTermite();
        public abstract void AjouteZone(params ZoneAbstrait[] zoneArray);
        public abstract void ChargerPersonnage(FabriqueAbstraite fab);
        public abstract void Repositioner();
        public abstract void FournirAcces();
        public abstract string Stats();
        public abstract void TourSuivant();
        public abstract void InitZones();
        public abstract void Avance();
        public abstract void Stop();

    }
}
