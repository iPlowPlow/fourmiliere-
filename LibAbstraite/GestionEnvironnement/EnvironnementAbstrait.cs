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
        public Boolean encours;
        private string titre;
        public int tourActuel = 0;
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
        public FabriqueAbstraite Fabrique;

        public int DimensionX { get; set; }
        public int DimensionY { get; set; }
        public List<AccesAbstrait> AccesList { get; set; }
        public ObservableCollection<ObjetAbstrait> ObjetList { get; set; }
        public ObservableCollection<PersonnageAbstrait> PersonnagesList { get; set; }
        public ObservableCollection<PersonnageAbstrait> PersonnagesMortList { get; set; }
        public ObservableCollection<ZoneAbstrait> ZoneList { get; set; }
        public abstract void AjouteOeuf(ObjetAbstrait unObject);
        public abstract void AjoutePheromone(ObjetAbstrait unObject);
        public abstract void AjouteNourriture(ObjetAbstrait unObject);
        public abstract void AjouterGuerriere();
        public abstract void AjouterReine();
        public abstract void AjouterOuvriere();
        public abstract void AjouterTermite();
        public abstract void AjouteZone(params ZoneAbstrait[] zoneArray);
        public abstract void ChargerEnv(FabriqueAbstraite fab);
        public abstract void ChargerPersonnage(FabriqueAbstraite fab);
        public abstract void ChargerObjet(FabriqueAbstraite fab);
        public abstract void DeplacerPersonnage(PersonnageAbstrait unPersonnage,ZoneAbstrait zdebut, ZoneAbstrait zfin);
        public abstract void Repositioner();
        public abstract void FournirAcces();
        public abstract string Simuler();
        public abstract string Statistiques();
        public abstract void TourSuivant();
        public abstract void InitZones();
        public abstract void Avance();
        public abstract void Stop();

    }
}
